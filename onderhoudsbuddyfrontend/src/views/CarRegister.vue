<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import api from './api.js';

const router = useRouter();
const licensePlate = ref('');
const mileage = ref(0);
const errorMessage = ref('');
const userId = ref(null);
const isLoading = ref(false);

onMounted(() => {
  const token = localStorage.getItem('token');
  if (!token) {
    router.push('/login');
    return;
  }

  const userInfoStr = localStorage.getItem('userInfo');
  if (!userInfoStr) {
    errorMessage.value = 'Gebruikersgegevens niet gevonden. Log opnieuw in.';
    return;
  }

  try {
    const userInfo = JSON.parse(userInfoStr);
    if (!userInfo.id) {
      errorMessage.value = 'Gebruikers-ID niet gevonden. Log opnieuw in.';
      return;
    }

    userId.value = userInfo.id;
    console.log('Gebruikers-ID geladen:', userId.value);
  } catch (error) {
    console.error('Fout bij het laden van gebruikersgegevens:', error);
    errorMessage.value = 'Probleem met gebruikersgegevens. Log opnieuw in.';
  }
});

const validateForm = () => {
  if (!licensePlate.value.trim()) {
    errorMessage.value = 'Kenteken is verplicht';
    return false;
  }

  if (mileage.value <= 0) {
    errorMessage.value = 'Kilometerstand moet groter zijn dan 0';
    return false;
  }

  return true;
};

const handleSubmit = async () => {
  errorMessage.value = '';

  if (!validateForm()) {
    return;
  }

  if (!userId.value) {
    errorMessage.value = 'Gebruiker niet geïdentificeerd. Log opnieuw in.';
    return;
  }

  isLoading.value = true;

  try {
    const carData = {
      licensePlate: licensePlate.value,
      mileage: parseInt(mileage.value)
    };

    console.log('Auto toevoegen voor gebruiker:', userId.value);
    console.log('Auto gegevens:', carData);

    await api.addCar(carData, userId.value);

    router.push('/my-cars');
  } catch (error) {
    console.error('Fout bij toevoegen auto:', error);
    errorMessage.value = error.response?.data?.message || 'Er is een fout opgetreden bij het toevoegen van de auto';
  } finally {
    isLoading.value = false;
  }
};
</script>

<template>
  <div class="add-car-container">
    <h2>Voeg een auto toe</h2>

    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>

    <form @submit.prevent="handleSubmit" class="car-form">
      <div class="form-group">
        <label for="licensePlate">Kenteken:</label>
        <input
          id="licensePlate"
          v-model="licensePlate"
          type="text"
          placeholder="Bijv. AB-123-C"
          required
        />
      </div>

      <div class="form-group">
        <label for="mileage">Kilometerstand:</label>
        <input
          id="mileage"
          v-model.number="mileage"
          type="number"
          min="1"
          required
        />
      </div>

      <div class="form-actions">
        <button
          type="button"
          @click="router.back()"
          class="cancel-button"
          :disabled="isLoading"
        >
          Annuleren
        </button>

        <button
          type="submit"
          class="submit-button"
          :disabled="isLoading"
        >
          {{ isLoading ? 'Bezig met toevoegen...' : 'Auto toevoegen' }}
        </button>
      </div>
    </form>
  </div>
</template>

<style scoped>
.add-car-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.error-message {
  background-color: #ffebee;
  color: #c62828;
  padding: 10px;
  border-radius: 4px;
  margin-bottom: 20px;
}

.car-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

label {
  font-weight: 500;
}

input {
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 16px;
}

.form-actions {
  display: flex;
  justify-content: space-between;
  margin-top: 10px;
}

button {
  padding: 12px 24px;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.cancel-button {
  background-color: #f5f5f5;
  color: #333;
}

.cancel-button:hover:not(:disabled) {
  background-color: #e0e0e0;
}

.submit-button {
  background-color: #1976d2;
  color: white;
}

.submit-button:hover:not(:disabled) {
  background-color: #1565c0;
}
</style>
