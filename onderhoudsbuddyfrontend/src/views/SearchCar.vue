<script setup>
import { ref } from 'vue';
import api from './api.js';


const licensePlate = ref('');
const car = ref(null);
const loading = ref(false);
const error = ref(null);
const searched = ref(false);

const searchCar = async () => {
  console.log('Zoekfunctie aangeroepen');

  if (!licensePlate.value.trim()) {
    error.value = 'Voer een kenteken in';
    return;
  }

  loading.value = true;
  error.value = null;
  car.value = null;
  searched.value = true;

  console.log('Zoeken naar kenteken:', licensePlate.value);
  console.log('API URL zal zijn:', `http://localhost:44393/api/Car/by-license/${licensePlate.value}`);

  try {
    console.log('API aanroep starten...');
    const response = await api.getCarByLicense(licensePlate.value);
    console.log('API response ontvangen:', response);
    car.value = response.data;
  } catch (err) {
    console.error('Volledige foutgegevens:', err);
  } finally {
    loading.value = false;
  }
};

const formatDate = (dateString) => {
  if (!dateString) return '-';
  try {
    const date = new Date(dateString);
    return new Intl.DateTimeFormat('nl-NL', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric'
    }).format(date);
  } catch {
    return dateString;
  }
};
</script>

<template>
  <div class="search-car-container">
    <h1>Auto zoeken op kenteken</h1>

    <div class="search-form">
      <div class="input-group">
        <input
            v-model="licensePlate"
            type="text"
            placeholder="Voer kenteken in"
            @keyup.enter="searchCar"
        />
        <button @click="searchCar" :disabled="loading">
          <span v-if="loading">Zoeken...</span>
          <span v-else>Zoek</span>
        </button>
      </div>

      <div v-if="error" class="error-message">
        {{ error }}
      </div>
    </div>

    <div v-if="loading" class="loading-indicator">
      <p>Voertuiggegevens ophalen...</p>
    </div>

    <div v-else-if="car" class="car-details">
      <h2>Voertuiggegevens</h2>

      <div class="car-info-grid">
        <div class="info-item">
          <span class="label">Kenteken:</span>
          <span class="value">{{ car.licensePlate }}</span>
        </div>

        <div class="info-item">
          <span class="label">Merk:</span>
          <span class="value">{{ car.brand }}</span>
        </div>

        <div class="info-item">
          <span class="label">Handelsbenaming:</span>
          <span class="value">{{ car.tradeName }}</span>
        </div>

        <div class="info-item">
          <span class="label">Voertuigsoort:</span>
          <span class="value">{{ car.vehicleType }}</span>
        </div>

        <div class="info-item">
          <span class="label">Kleur:</span>
          <span class="value">{{ car.primaryColor }}</span>
        </div>

        <div class="info-item">
          <span class="label">APK vervaldatum:</span>
          <span class="value">{{ formatDate(car.apkExpiryDate) }}</span>
        </div>

        <div class="info-item">
          <span class="label">Massa (kg):</span>
          <span class="value">{{ car.emptyVehicleMass }}</span>
        </div>

        <div class="info-item">
          <span class="label">Eerste registratie NL:</span>
          <span class="value">{{ formatDate(car.registrationDate) }}</span>
        </div>

        <div class="info-item">
          <span class="label">Eerste toelating:</span>
          <span class="value">{{ formatDate(car.firstAdmissionDate) }}</span>
        </div>

        <div class="info-item">
          <span class="label">Tellerstandoordeel:</span>
          <span class="value">{{ car.mileageJudgment }}</span>
        </div>
      </div>
    </div>

    <div v-else-if="searched && !error" class="no-results">
      <p>Geen voertuiggegevens gevonden</p>
    </div>
  </div>
</template>

<style scoped>
.search-car-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

h1 {
  color: black;
  border-bottom: 2px solid #eaeaea;
  padding-bottom: 10px;
  margin-bottom: 20px;
}

.search-form {
  margin-bottom: 20px;
}

.input-group {
  display: flex;
  gap: 10px;
}

input {
  flex: 1;
  padding: 10px 12px;
  border: 1px solid #000000;
  border-radius: 4px;
  font-size: 16px;
}

button {
  background-color: #4267B2;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 10px 20px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.2s;
}

button:hover {
  background-color: #3457a8;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.error-message {
  color: #e74c3c;
  background-color: #ffeaea;
  padding: 10px;
  border-radius: 4px;
  margin-top: 15px;
  border: 1px solid #ffcccc;
}

.car-details {
  background-color: #f8f9fa;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.car-details h2 {
  color: black;
  margin-top: 0;
  margin-bottom: 20px;
  border-bottom: 1px solid #ddd;
  padding-bottom: 10px;
}

.car-info-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 15px;
}

.info-item {
  display: flex;
  flex-direction: column;
  background-color: white;
  padding: 10px 15px;
  border-radius: 4px;
  border-left: 3px solid #4267B2;
}

.label {
  font-size: 12px;
  color: black;
  margin-bottom: 4px;
}

.value {
  color: #4267B2;
  font-size: 16px;
  font-weight: 500;
}

.loading-indicator {
  text-align: center;
  padding: 20px;
  color: black;
}

.no-results {
  text-align: center;
  padding: 30px;
  background-color: #f8f9fa;
  border-radius: 8px;
  color: black;
}
</style>
