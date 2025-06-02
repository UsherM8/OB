<template>
  <div class="login-container">
    <div class="login-form">
      <h2>Inloggen</h2>

      <div v-if="errorMessage" class="error-message">
        {{ errorMessage }}
      </div>

      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label for="email">E-mailadres</label>
          <input
              name="email"
            id="email"
            type="email"
            v-model="email"
            placeholder="Uw e-mailadres"
            required
            class="input-focus"
            autocomplete="email"
          />
        </div>

        <div class="form-group">
          <label for="password">Wachtwoord</label>
          <input
              name="password"
            id="password"
            type="password"
            v-model="password"
            placeholder="Uw wachtwoord"
            required
            class="input-focus"
            autocomplete="current-password"
          />
        </div>

        <button type="submit" class="login-button" :disabled="isLoading">
          {{ isLoading ? 'Bezig met inloggen...' : 'Inloggen' }}
        </button>
      </form>

      <div class="register-link">
        Nog geen account? <router-link to="/register">Registreer hier</router-link>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import api from './api.js';

const email = ref('');
const password = ref('');
const errorMessage = ref('');
const isLoading = ref(false);
const router = useRouter();

const handleLogin = async () => {
  if (!email.value || !password.value) {
    errorMessage.value = 'Vul zowel e-mail als wachtwoord in.';
    return;
  }

  isLoading.value = true;
  errorMessage.value = '';

  try {
    console.log('Inlogpoging voor:', email.value);
    const response = await api.login(email.value, password.value);
    console.log('API respons:', response);

    const token = response.data.token;
    localStorage.setItem('token', token);
    console.log('Token opgeslagen in localStorage:', token);

    const userId = response.data.userId;

    const userData = {
      id: userId,
      email: email.value
    };

    localStorage.setItem('userInfo', JSON.stringify(userData));
    console.log('Gebruikersgegevens opgeslagen:', userData);

    api.setAuthToken(token);

    console.log('Succesvol ingelogd als', email.value);
    router.push('/home');
  } catch (error) {
    console.error('Inlogfout:', error);
    errorMessage.value = 'Ongeldige e-mail of wachtwoord.';
  } finally {
    isLoading.value = false;
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 80vh;
  padding: 20px;
}

.login-form {
  width: 100%;
  max-width: 400px;
  padding: 30px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

h2 {
  text-align: center;
  margin-bottom: 24px;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: 500;
  color: #333;
}

input {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 16px;
  transition: border-color 0.3s;
}

.input-focus:focus {
  outline: none;
  border-color: #4f46e5;
  box-shadow: 0 0 0 2px rgba(79, 70, 229, 0.2);
}

.login-button {
  width: 100%;
  padding: 12px;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.3s;
}

.login-button:hover {
  background-color: #4338ca;
}

.login-button:disabled {
  background-color: #a5a5a5;
  cursor: not-allowed;
}

.register-link {
  margin-top: 20px;
  text-align: center;
  font-size: 14px;
}

.register-link a {
  color: #4f46e5;
  text-decoration: none;
}

.register-link a:hover {
  text-decoration: underline;
}

.error-message {
  background-color: #fee2e2;
  color: #b91c1c;
  padding: 12px;
  border-radius: 4px;
  margin-bottom: 20px;
  font-size: 14px;
}
</style>
