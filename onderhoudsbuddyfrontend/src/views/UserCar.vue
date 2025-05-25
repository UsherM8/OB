<script setup>
import { ref, onMounted } from 'vue';
import api from './api.js';

const cars = ref([]);
const loading = ref(false);
const error = ref(null);

const userId = ref(localStorage.getItem('userInfo')
  ? JSON.parse(localStorage.getItem('userInfo')).id
  : null);

const fetchUserCars = async () => {
  loading.value = true;
  error.value = null;

  if (!userId.value) {
    error.value = 'Geen gebruikers-ID gevonden. Log opnieuw in.';
    loading.value = false;
    return;
  }

  try {
    console.log('Auto\'s ophalen voor gebruiker:', userId.value);
    const response = await api.getUserCars(userId.value);
    console.log('API response ontvangen:', response);
    cars.value = response.data;
  } catch (err) {
    console.error('Fout bij ophalen auto\'s:', err);
    error.value = 'Er is een fout opgetreden bij het ophalen van de auto\'s';
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchUserCars();
});

const formatDate = (dateString) => {
  if (!dateString) return '-';

  try {
    const year = dateString.substring(0, 4);
    const month = dateString.substring(4, 6);
    const day = dateString.substring(6, 8);

    return `${day}-${month}-${year}`;
  } catch {
    return dateString;
  }
};

const calculateCarAge = (dateString) => {
  if (!dateString) return '-';

  try {
    const year = parseInt(dateString.substring(0, 4));
    const month = parseInt(dateString.substring(4, 6)) - 1;
    const day = parseInt(dateString.substring(6, 8));

    const firstDate = new Date(year, month, day);
    const today = new Date();

    let age = today.getFullYear() - firstDate.getFullYear();
    const monthDiff = today.getMonth() - firstDate.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < firstDate.getDate())) {
      age--;
    }

    return `${age} jaar`;
  } catch {
    return '-';
  }
};

const formatMileage = (mileage) => {
  if (mileage === undefined || mileage === null) return '-';
  return new Intl.NumberFormat('nl-NL').format(mileage) + ' km';
};

const formatWeight = (weight) => {
  if (weight === undefined || weight === null) return '-';
  return new Intl.NumberFormat('nl-NL').format(weight) + ' kg';
};
</script>

<template>
  <div class="user-cars-container">
    <h1>Mijn Auto's</h1>

    <div v-if="loading" class="loading-indicator">
      <p>Auto's worden geladen...</p>
    </div>

    <div v-else-if="error" class="error-message">
      {{ error }}
      <button @click="fetchUserCars" class="retry-button">Opnieuw proberen</button>
    </div>

    <div v-else-if="cars.length === 0" class="no-cars">
      <p>Je hebt nog geen auto's toegevoegd.</p>
      <router-link to="/search-car" class="add-car-link">Auto toevoegen</router-link>
    </div>

    <div v-else class="cars-grid">
      <div v-for="car in cars" :key="car.carId" class="car-card">
        <div class="car-details">
          <div class="license-plate">{{ car.licensePlate }}</div>

          <div class="car-header">
            <div class="brand-model">
              {{ car.brand }} {{ car.tradeName }}
            </div>
            <div class="car-age">
              {{ calculateCarAge(car.firstAdmissionDate) }}
            </div>
          </div>

          <div class="car-specs">
            <div class="spec-item">
              <div class="spec-icon">🚗</div>
              <div class="spec-text">{{ car.vehicleType }}</div>
            </div>
            <div class="spec-item">
              <div class="spec-icon">🎨</div>
              <div class="spec-text">{{ car.primaryColor }}</div>
            </div>
            <div class="spec-item">
              <div class="spec-icon">📏</div>
              <div class="spec-text">{{ formatMileage(car.mileage) }}</div>
            </div>
            <div class="spec-item">
              <div class="spec-icon">⚖️</div>
              <div class="spec-text">{{ formatWeight(car.emptyVehicleMass) }}</div>
            </div>
          </div>

          <div class="info-grid">
            <div class="info-item">
              <span class="label">APK geldig tot:</span>
              <span class="value">{{ formatDate(car.apkExpiryDate) }}</span>
            </div>

            <div class="info-item">
              <span class="label">Eerste toelating:</span>
              <span class="value">{{ formatDate(car.firstAdmissionDate) }}</span>
            </div>

            <div class="info-item">
              <span class="label">Kilometerstand oordeel:</span>
              <span class="value">{{ car.mileageJudgment }}</span>
            </div>
          </div>

          <div class="car-actions">
            <router-link :to="`/car-details/${car.carId}`" class="action-button details-button">
              Details bekijken
            </router-link>
            <router-link :to="`/car-edit/${car.carId}`" class="action-button edit-button">
              Bewerken
            </router-link>
          </div>
        </div>
      </div>
    </div>

    <div class="add-car-container">
      <router-link to="/search-car" class="add-car-button">
        <span class="add-icon">+</span> Auto toevoegen
      </router-link>
    </div>
  </div>
</template>

<style scoped>
/* Hier blijft de CSS hetzelfde als in het vorige voorbeeld */
.user-cars-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  position: relative;
}

h1 {
  color: black;
  border-bottom: 2px solid #eaeaea;
  padding-bottom: 10px;
  margin-bottom: 30px;
}

.loading-indicator, .error-message, .no-cars {
  text-align: center;
  padding: 30px;
  background-color: #f8f9fa;
  border-radius: 8px;
  margin: 20px 0;
}

.error-message {
  color: #e74c3c;
  background-color: #ffeaea;
  border: 1px solid #ffcccc;
}

.retry-button {
  background-color: #4267B2;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 8px 16px;
  margin-top: 10px;
  cursor: pointer;
}

.add-car-link {
  display: inline-block;
  background-color: #4267B2;
  color: white;
  text-decoration: none;
  padding: 10px 20px;
  border-radius: 4px;
  margin-top: 15px;
}

.cars-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 20px;
  margin-bottom: 70px;
}

.car-card {
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.car-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.15);
}

.car-details {
  padding: 15px;
}

.license-plate {
  background-color: #f8ca00;
  color: black;
  font-weight: bold;
  text-align: center;
  padding: 8px;
  border-radius: 4px;
  margin-bottom: 15px;
  font-size: 18px;
  letter-spacing: 1px;
  border: 1px solid #000;
}

.car-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
}

.brand-model {
  font-size: 20px;
  font-weight: 600;
  color: #333;
}

.car-age {
  background-color: #edf2f7;
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 13px;
  color: #4a5568;
}

.car-specs {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-bottom: 15px;
  padding-bottom: 15px;
  border-bottom: 1px solid #eaeaea;
}

.spec-item {
  display: flex;
  align-items: center;
  background-color: #f7fafc;
  padding: 6px 10px;
  border-radius: 4px;
  font-size: 13px;
}

.spec-icon {
  margin-right: 6px;
  font-size: 15px;
}

.spec-text {
  color: #4a5568;
}

.info-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  margin-bottom: 15px;
}

.info-item {
  display: flex;
  flex-direction: column;
  padding: 8px;
  border-radius: 4px;
  background-color: #f9f9f9;
}

.label {
  font-size: 12px;
  color: #666;
  margin-bottom: 4px;
}

.value {
  font-size: 14px;
  color: #333;
  font-weight: 500;
}

.car-actions {
  display: flex;
  gap: 10px;
  margin-top: 15px;
}

.action-button {
  padding: 10px 16px;
  text-decoration: none;
  border-radius: 4px;
  font-size: 14px;
  flex: 1;
  text-align: center;
  transition: background-color 0.2s, transform 0.1s;
}

.details-button {
  background-color: #4267B2;
  color: white;
}

.details-button:hover {
  background-color: #3457a8;
  transform: translateY(-2px);
}

.edit-button {
  background-color: #f1f1f1;
  color: #333;
  border: 1px solid #ddd;
}

.edit-button:hover {
  background-color: #e8e8e8;
  transform: translateY(-2px);
}

.add-car-container {
  position: fixed;
  bottom: 30px;
  right: 30px;
  z-index: 100;
}

.add-car-button {
  display: flex;
  align-items: center;
  background-color: #4267B2;
  color: white;
  padding: 12px 20px;
  border-radius: 30px;
  text-decoration: none;
  font-weight: bold;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  transition: transform 0.2s, background-color 0.2s;
}

.add-car-button:hover {
  background-color: #3457a8;
  transform: translateY(-3px);
}

.add-icon {
  font-size: 20px;
  margin-right: 8px;
}

@media (max-width: 768px) {
  .cars-grid {
    grid-template-columns: 1fr;
  }

  .info-grid {
    grid-template-columns: 1fr;
  }
}
</style>
