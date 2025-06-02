<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import api from './api.js';

const route = useRoute();
const carId = parseInt(route.params.carId);

const services = ref([]);
const loading = ref(false);
const error = ref(null);

const fetchServices = async () => {
  loading.value = true;
  error.value = null;

  try {
    const response = await api.getAllServicesById(carId);
    services.value = response.data;
  } catch (err) {
    console.error('Fout bij ophalen services:', err);
    error.value = 'Er is een fout opgetreden bij het ophalen van de services.';
  } finally {
    loading.value = false;
  }
};

const formatDate = (dateString) => {
  if (!dateString) return '-';
  return new Date(dateString).toLocaleDateString('nl-NL');
};

onMounted(() => {
  fetchServices();
});
</script>

<template>
  <div class="car-service-container">
    <h2>Services voor auto {{ carId }}</h2>

    <div v-if="loading" class="loading">Services worden geladen...</div>
    <div v-else-if="error" class="error">{{ error }}</div>
    <div v-else-if="services.length === 0" class="no-services">Geen services gevonden voor deze auto.</div>

    <div v-else class="services-list">
      <div v-for="service in services" :key="service.serviceId" class="service-card">
        <div class="service-header">
          <strong>Type:</strong> {{ service.serviceType }}<br />
          <strong>Datum:</strong> {{ formatDate(service.serviceDate) }}
        </div>

        <router-link :to="`/Service/${service.serviceId}`" class="details-link-button">
          Bekijk volledige service
        </router-link>
      </div>
    </div>
  </div>
</template>

<style scoped>
.car-service-container {
  max-width: 800px;
  margin: 40px auto;
  padding: 20px;
  background-color: #f7f9fc;
  border-radius: 8px;
  font-family: 'Segoe UI', sans-serif;
  color: black;
}

h2 {
  margin-bottom: 20px;
  color: black;
}

.loading,
.error,
.no-services {
  text-align: center;
  margin-top: 20px;
  color: black;
}

.services-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
  color: black;
}

.service-card {
  background-color: #fff;
  border: 1px solid #ddd;
  padding: 16px;
  border-radius: 6px;
  box-shadow: 0 1px 4px rgba(0,0,0,0.05);
  color: black;
}

.service-header {
  margin-bottom: 10px;
  color: black;
}

.details-link-button {
  display: inline-block;
  background-color: #4267B2;
  color: white;
  padding: 8px 12px;
  border-radius: 4px;
  text-decoration: none;
  font-weight: bold;
}

.details-link-button:hover {
  background-color: #3457a8;
}
</style>
