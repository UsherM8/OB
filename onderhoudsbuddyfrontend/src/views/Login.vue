<template>
  <div class="login-container">
    <div class="login-card">
      <h2 class="title">Welkom terug</h2>

      <form @submit.prevent="handleLogin" class="login-form">
        <div class="form-group">
          <label for="email">E-mail</label>
          <div class="input-container">
            <input
                id="email"
                type="email"
                v-model="email"
                placeholder="Uw e-mailadres"
                required
                :class="{ 'input-focus': activeInput === 'email' }"
                @focus="activeInput = 'email'"
                @blur="activeInput = null"
            />
          </div>
        </div>

        <div class="form-group">
          <label for="password">Wachtwoord</label>
          <div class="input-container">
            <input
                id="password"
                :type="showPassword ? 'text' : 'password'"
                v-model="password"
                placeholder="Uw wachtwoord"
                required
                :class="{ 'input-focus': activeInput === 'password' }"
                @focus="activeInput = 'password'"
                @blur="activeInput = null"
            />
<!--            <button-->
<!--                type="button"-->
<!--                class="toggle-password"-->
<!--                @click="showPassword = !showPassword"-->
<!--            >-->
<!--              {{ showPassword ? '🙈' : '👁️' }}-->
<!--            </button>-->
          </div>
        </div>

        <button
            type="submit"
            class="login-button"
            :class="{ 'button-loading': isLoading }"
            :disabled="isLoading"
        >
          <span v-if="!isLoading">Inloggen</span>
          <span v-else class="loading-spinner"></span>
        </button>
      </form>

      <p class="register-link">
        Nog geen account? <router-link to="/register">Registreren</router-link>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const email = ref('');
const password = ref('');
const router = useRouter();
const showPassword = ref(false);
const isLoading = ref(false);
const activeInput = ref(null);

const handleLogin = async () => {
  if (email.value && password.value) {
    isLoading.value = true;

    try {
      await new Promise(resolve => setTimeout(resolve, 1500));

      console.log(`Ingelogd als ${email.value}`);
      router.push('/home');
    } catch (error) {
      alert('Er is een fout opgetreden tijdens het inloggen.');
    } finally {
      isLoading.value = false;
    }
  } else {
    alert('Vul alle velden in.');
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  padding: 40px;
  font-family: 'Arial', sans-serif;
  background-color: #f8f9fa;
}

.login-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 15px 25px rgba(0, 0, 0, 0.1);
  padding: 40px;
  width: 100%;
  max-width: 550px;
  min-width: 320px;
  min-height: 500px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  transition: transform 0.3s, box-shadow 0.3s;
}

.login-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 20px 30px rgba(0, 0, 0, 0.15);
}

.title {
  color: #333;
  margin-bottom: 40px;
  text-align: center;
  font-weight: 600;
  font-size: 2rem;
}

.form-group {
  margin-bottom: 30px;
}

label {
  display: block;
  margin-bottom: 10px;
  font-weight: 500;
  color: #555;
  font-size: 1rem;
}

.input-container {
  position: relative;
  display: flex;
  align-items: center;
}

.icon {
  position: absolute;
  left: 15px;
  color: #aaa;
  font-size: 1.2rem;
}

input {
  width: 100%;
  padding: 15px 15px 15px 45px;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1.1rem;
  transition: all 0.3s;
}

input:focus {
  outline: none;
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.15);
}

.input-focus {
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.15);
}

.toggle-password {
  position: absolute;
  right: 15px;
  background: none;
  border: none;
  cursor: pointer;
  font-size: 1.2rem;
  color: #777;
}

.login-button {
  width: 100%;
  padding: 15px;
  background: linear-gradient(to right, #667eea, #764ba2);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s;
  position: relative;
  margin-top: 20px;
}

.login-button:hover:not(:disabled) {
  background: linear-gradient(to right, #5a6eee, #6a3f9e);
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(102, 126, 234, 0.3);
}

.login-button:active:not(:disabled) {
  transform: translateY(0);
}

.login-button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.button-loading {
  color: transparent;
}

.loading-spinner {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 24px; /* Grotere spinner */
  height: 24px; /* Grotere spinner */
  border: 3px solid rgba(255, 255, 255, 0.3);
  border-radius: 50%;
  border-top-color: #fff;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to { transform: translate(-50%, -50%) rotate(360deg); }
}

.register-link {
  text-align: center;
  margin-top: 30px; /* Meer ruimte */
  font-size: 1rem; /* Grotere tekst */
  color: #555;
}

.register-link a {
  color: #667eea;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.2s;
}

.register-link a:hover {
  color: #764ba2;
  text-decoration: underline;
}

/* Media queries voor responsive design */
@media (max-height: 700px) {
  .login-card {
    min-height: auto;
    padding: 30px;
  }

  .title {
    margin-bottom: 25px;
  }

  .form-group {
    margin-bottom: 20px;
  }
}

@media (max-width: 480px) {
  .login-container {
    padding: 20px;
  }

  .login-card {
    padding: 25px;
  }

  .title {
    font-size: 1.8rem;
  }

  input {
    padding: 12px 12px 12px 40px;
  }
}
</style>
