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


const router = useRouter();
const email = ref('');
const password = ref('');
const errorMessage = ref('');
const isLoading = ref(false);

async function handleLogin() {
  if (isLoading.value) return;

  errorMessage.value = '';
  isLoading.value = true;

  try {
    // Debug: Log inlogpoging
    console.log('Inlogpoging voor:', email.value);

    // Gebruik je API service in plaats van directe axios call
    const response = await api.login(email.value, password.value);

    // Debug: Log volledige respons
    console.log('API respons:', response.data);

    // Controleer of de inlogpoging succesvol was
    if (response.data && response.data.token) {
      // Sla token op in localStorage
      localStorage.setItem('token', response.data.token);

      // Stel de auth token in voor toekomstige requests
      api.setAuthToken(response.data.token);

      // Debug: Controleer of de token correct is opgeslagen
      console.log('Token opgeslagen in localStorage:', localStorage.getItem('token'));

      // Sla eventueel gebruikersgegevens op als die beschikbaar zijn
      if (response.data.user) {
        localStorage.setItem('user', JSON.stringify(response.data.user));
      }

      console.log('Succesvol ingelogd als', email.value);

      // Navigeer naar de homepagina
      router.push('/home');
    } else {
      // De server gaf een succesvolle respons, maar zonder token
      console.error('Geen token ontvangen in de respons:', response.data);
      errorMessage.value = 'Er is een probleem met inloggen. Probeer het opnieuw.';
    }
  } catch (error) {
    // Er is een fout opgetreden bij het maken van het verzoek
    console.error('Login fout:', error);

    // Toon een gepast foutbericht afhankelijk van de fout
    if (error.response) {
      // De server gaf een foutrespons (401, 403, etc.)
      console.error('Server foutrespons:', error.response.data);

      if (error.response.status === 401) {
        errorMessage.value = 'Ongeldige inloggegevens. Controleer je e-mail en wachtwoord.';
      } else if (error.response.status === 429) {
        errorMessage.value = 'Te veel inlogpogingen. Probeer het later opnieuw.';
      } else {
        errorMessage.value = 'Inloggen mislukt: ' + (error.response.data.message || 'Onbekende fout');
      }
    } else if (error.request) {
      // Het verzoek werd gemaakt maar er kwam geen antwoord
      console.error('Geen antwoord van server:', error.request);
      errorMessage.value = 'Geen antwoord van de server. Controleer je internetverbinding.';
    } else {
      // Er is iets misgegaan bij het opzetten van het verzoek
      errorMessage.value = 'Er is een fout opgetreden. Probeer het later opnieuw.';
    }
  } finally {
    isLoading.value = false;
  }
}
</script>

<style scoped>
/* Bestaande CSS behouden */
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
