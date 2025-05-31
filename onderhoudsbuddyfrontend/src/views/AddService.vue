<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import api from './api.js';

const route = useRoute();
const router = useRouter();

const carId = parseInt(route.params.carId); // Alleen carId uit URL

const garages = ref([]);
const searchQuery = ref('');
const error = ref(null);
const loading = ref(false);

const service = ref({
  carId: carId,
  garageId: null,
  serviceType: '',
  status: '',
  serviceDate: '',
  nextServiceDate: '',
  description: ''
});

const filteredGarages = computed(() => {
  return garages.value.filter(g =>
      g.name.toLowerCase().includes(searchQuery.value.toLowerCase())
  );
});

const fetchGarages = async () => {
  try {
    const response = await api.getAllGarages();
    garages.value = response.data;
  } catch (err) {
    console.error('Fout bij ophalen garages:', err);
    error.value = 'Kon garages niet ophalen.';
  }
};

const createService = async () => {
  if (!service.value.garageId) {
    error.value = 'Selecteer een garage.';
    return;
  }

  try {
    loading.value = true;
    error.value = null;

    await api.createService({
      carId: service.value.carId,
      garageId: service.value.garageId,
      serviceType: service.value.serviceType,
      status: service.value.status,
      serviceDate: new Date(service.value.serviceDate),
      nextServiceDate: new Date(service.value.nextServiceDate),
      description: service.value.description
    });

    router.push('/UserCar');
  } catch (err) {
    console.error('Fout bij aanmaken service:', err);
    error.value = 'Aanmaken service mislukt.';
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchGarages();
});
</script>

<template>
  <div class="form-container">
    <h2>Service toevoegen aan auto {{ carId }}</h2>

    <div v-if="error" class="error">{{ error }}</div>

    <label for="garage-search">Garage zoeken:</label>
    <input
        id="garage-search"
        v-model="searchQuery"
        placeholder="Typ om een garage te zoeken..."
        type="text"
    />

    <ul v-if="filteredGarages.length" class="garage-dropdown">
      <li
          v-for="garage in filteredGarages"
          :key="garage.garageId"
          @click="() => { service.garageId = garage.garageId; searchQuery = garage.name }"
      >
        {{ garage.name }}
      </li>
    </ul>

    <label>Service type:</label>
    <input type="text" v-model="service.serviceType" placeholder="Bijv. Olie verversen" />

    <label>Status:</label>
    <input type="text" v-model="service.status" placeholder="Bijv. In behandeling" />

    <label>Service datum:</label>
    <input type="date" v-model="service.serviceDate" />

    <label>Volgende service datum:</label>
    <input type="date" v-model="service.nextServiceDate" />

    <label>Beschrijving:</label>
    <textarea v-model="service.description" rows="3" placeholder="Omschrijving van de service" />

    <button @click="createService" :disabled="loading">
      {{ loading ? 'Toevoegen...' : 'Service toevoegen' }}
    </button>
  </div>
</template>

<style scoped>
.form-container {
  max-width: 600px;
  margin: 40px auto;
  padding: 20px;
  background-color: #f7f9fc;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  font-family: Arial, sans-serif;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

label {
  display: block;
  margin-top: 15px;
  font-weight: bold;
  color: #000;
}

input,
textarea {
  width: 100%;
  padding: 10px;
  margin-top: 6px;
  border: 1px solid #ccc;
  border-radius: 4px;
  color: #000;
  background-color: #fff;
}

textarea {
  resize: vertical;
}

.garage-dropdown {
  max-height: 150px;
  overflow-y: auto;
  margin-top: 5px;
  padding: 0;
  list-style: none;
  border: 1px solid #ccc;
  background: white;
  border-radius: 4px;
}

.garage-dropdown li {
  padding: 10px;
  cursor: pointer;
}

.garage-dropdown li:hover {
  background-color: #e6f0ff;
}

button {
  margin-top: 20px;
  width: 100%;
  padding: 12px;
  background-color: #4267B2;
  color: white;
  border: none;
  border-radius: 4px;
  font-weight: bold;
  cursor: pointer;
}

button:disabled {
  background-color: #888;
  cursor: not-allowed;
}

.error {
  color: red;
  margin-bottom: 10px;
}
</style>
