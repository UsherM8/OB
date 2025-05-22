<template>
  <div class="register-container">
    <div class="register-card">
      <h2 class="title">Meld aan</h2>

      <form @submit.prevent="handleRegister" class="register-form">
        <div class="form-group">
          <label for="firstName">Voornaam</label>
          <div class="input-container">
            <input
                id="firstName"
                type="text"
                v-model="firstName"
                placeholder="Uw voornaam"
                required
                :class="{ 'input-focus': activeInput === 'firstName' }"
                @focus="activeInput = 'firstName'"
                @blur="activeInput = null"
            />
          </div>
        </div>

        <div class="form-group">
          <label for="lastName">Achternaam</label>
          <div class="input-container">
            <input
                id="lastName"
                type="text"
                v-model="lastName"
                placeholder="Uw achternaam"
                required
                :class="{ 'input-focus': activeInput === 'lastName' }"
                @focus="activeInput = 'lastName'"
                @blur="activeInput = null"
            />
          </div>
        </div>

        <div class="form-group">
          <label for="birthDate">Geboortedatum</label>
          <div class="input-container">
            <input
                id="birthDate"
                type="date"
                v-model="birthDate"
                required
                :class="{ 'input-focus': activeInput === 'birthDate' }"
                @focus="activeInput = 'birthDate'"
                @blur="activeInput = null"
            />
          </div>
        </div>

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
            <button
                type="button"
                class="toggle-password"
                @click="showPassword = !showPassword"
            >
              {{ showPassword ? 'Verbergen' : 'Tonen' }}
            </button>
          </div>
        </div>

        <div v-if="errorMessage" class="error-message">
          {{ errorMessage }}
        </div>

        <button
            type="submit"
            class="register-button"
            :class="{ 'button-loading': isLoading }"
            :disabled="isLoading"
        >
          <span v-if="!isLoading">Registreren</span>
          <span v-else class="loading-spinner"></span>
        </button>
      </form>

      <p class="login-link">
        Heeft u al een account? <router-link to="/">Inloggen</router-link>
      </p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import api from './api.js';

const firstName = ref('');
const lastName = ref('');
const birthDate = ref('');
const email = ref('');
const password = ref('');
const type = ref('USER');
const router = useRouter();
const showPassword = ref(false);
const isLoading = ref(false);
const activeInput = ref(null);
const errorMessage = ref('');

const handleRegister = async () => {
  if (firstName.value && lastName.value && birthDate.value && email.value && password.value) {
    isLoading.value = true;
    errorMessage.value = '';

    try {
      // Maak een gebruiker aan met de register functie
      const userData = {
        firstName: firstName.value,
        lastName: lastName.value,
        birthDate: birthDate.value,
        email: email.value,
        password: password.value,
        type: type.value
      };

      const response = await api.createUser(userData);

      console.log(`Succesvol geregistreerd als ${firstName.value} ${lastName.value}`);

      // Naar login pagina sturen:
      router.push('/');

    } catch (error) {
      console.error('Registratie fout:', error);

      // Toon een gebruiksvriendelijke foutmelding
      if (error.response) {
        // De server heeft geantwoord met een foutstatuscode
        if (error.response.status === 409) {
          errorMessage.value = 'Dit e-mailadres is al in gebruik. Probeer in te loggen of gebruik een ander e-mailadres.';
        } else if (error.response.status === 400) {
          errorMessage.value = 'Ongeldige gegevens. Controleer alstublieft uw invoer.';
        } else {
          errorMessage.value = 'Er is een fout opgetreden bij het registreren.';
        }
      } else if (error.request) {
        // De request was gemaakt maar er was geen antwoord
        errorMessage.value = 'Geen verbinding met de server. Probeer het later opnieuw.';
      } else {
        // Er is iets misgegaan bij het opzetten van het verzoek
        errorMessage.value = 'Er is een fout opgetreden. Probeer het opnieuw.';
      }
    } finally {
      isLoading.value = false;
    }
  } else {
    errorMessage.value = 'Vul alle velden in.';
  }
};
</script>

<style scoped>
.register-container {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  padding: 40px;
  font-family: 'Arial', sans-serif;
  background-color: #f8f9fa;
}

.register-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 15px 25px rgba(0, 0, 0, 0.1);
  padding: 40px;
  width: 100%;
  max-width: 550px;
  min-width: 320px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  transition: transform 0.3s, box-shadow 0.3s;
}

.register-card:hover {
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
  margin-bottom: 20px;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: 500;
  color: #555;
  font-size: 1rem;
}

.input-container {
  position: relative;
  display: flex;
  align-items: center;
}

input, select {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
  transition: all 0.3s;
}

input:focus, select:focus {
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
  font-size: 0.9rem;
  color: #555;
}

.register-button {
  width: 100%;
  padding: 15px;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s;
  margin-top: 20px;
  position: relative;
}

.register-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 7px 14px rgba(50, 50, 93, 0.1), 0 3px 6px rgba(0, 0, 0, 0.08);
}

.register-button:active {
  transform: translateY(1px);
}

.button-loading {
  color: transparent;
}

.loading-spinner {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 20px;
  height: 20px;
  border: 3px solid rgba(255, 255, 255, 0.3);
  border-radius: 50%;
  border-top-color: white;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to {
    transform: translate(-50%, -50%) rotate(360deg);
  }
}

.login-link {
  margin-top: 30px;
  text-align: center;
  color: #666;
  font-size: 1rem;
}

.login-link a {
  color: #667eea;
  text-decoration: none;
  font-weight: 600;
  transition: color 0.3s;
}

.login-link a:hover {
  color: #764ba2;
}

.error-message {
  color: #e53e3e;
  margin-bottom: 20px;
  padding: 10px;
  background-color: #fff5f5;
  border-radius: 6px;
  font-size: 0.9rem;
  text-align: center;
}
</style>
