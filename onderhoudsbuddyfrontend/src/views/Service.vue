<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import api from './api.js';

const route = useRoute();
const serviceId = Number(route.params.serviceId);

const service = ref(null);
const car = ref(null);
const garage = ref(null);
const error = ref(null);
const loading = ref(false);

const fetchData = async () => {
  loading.value = true;
  try {
    const serviceResponse = await api.getServiceById(serviceId);
    service.value = serviceResponse.data;

    const carResponse = await api.getCarById(service.value.carId);
    car.value = carResponse.data;

    const garageResponse = await api.getGarageById(service.value.garageId);
    garage.value = garageResponse.data;
  } catch (err) {
    console.error(err);
    error.value = 'Kon gegevens niet laden.';
  } finally {
    loading.value = false;
  }
};

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

onMounted(fetchData);
</script>

<template>
  <div class="service-details">
    <div v-if="error" class="error">{{ error }}</div>
    <div v-else-if="loading">Laden...</div>
    <div v-else-if="service && car && garage">
      <h2> Onderhoud {{ car.brand }} {{ car.tradeName }}ccccc </h2>

      <section>
        <h3>Onderhoud informatie</h3>
        <p><strong>Type:</strong> {{ service.serviceType }}</p>
        <p><strong>Status:</strong> {{ service.status }}</p>
        <p><strong>Beschrijving:</strong> {{ service.description }}</p>
        <p><strong>Service datum:</strong> {{ new Date(service.serviceDate).toLocaleDateString() }}</p>
        <p><strong>Volgende service:</strong> {{ new Date(service.nextServiceDate).toLocaleDateString() }}</p>
      </section>

      <section>
        <h3>Auto informatie</h3>
        <p><strong>Kenteken:</strong> {{ car.licensePlate }}</p>
        <p><strong>Merk:</strong> {{ car.brand }}</p>
        <p><strong>Handelsnaam:</strong> {{ car.tradeName }}</p>
        <p><strong>Voertuigtype:</strong> {{ car.vehicleType }}</p>
        <p><strong>Kleur:</strong> {{ car.primaryColor }}</p>
        <p><strong>Kilometerstand:</strong> {{ car.mileage }} km</p>
        <p><strong>Leeggewicht:</strong> {{ car.emptyVehicleMass }} kg</p>
        <p><strong>APK geldig tot:</strong> {{ formatDate(car.apkExpiryDate) }}</p>
        <p><strong>Registratiedatum:</strong> {{ formatDate(car.registrationDate) }}</p>
        <p><strong>Eerste toelating:</strong> {{ formatDate(car.firstAdmissionDate) }}</p>
        <p><strong>Oordeel kilometerstand:</strong> {{ car.mileageJudgment }}</p>
      </section>

      <section>
        <h3>Garage informatie</h3>
        <p><strong>Naam:</strong> {{ garage.name }}</p>
        <p><strong>Adres:</strong> {{ garage.streetName }} {{ garage.houseNumber }}, {{ garage.postalCode }} {{ garage.city }}</p>
        <p><strong>Extra info:</strong> {{ garage.extraAddressInfo }}</p>
        <p><strong>Telefoon:</strong> {{ garage.phoneNumber }}</p>
        <p><strong>Email:</strong> {{ garage.email }}</p>
      </section>
    </div>
  </div>
</template>

<style scoped>
.service-details {
  max-width: 800px;
  margin: 40px auto;
  padding: 20px;
  background-color: #f7f9fc;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  color: black;
}

h2 {
  margin-bottom: 20px;
}

h3 {
  margin-top: 30px;
  color: #333;
}

p {
  margin: 6px 0;
  line-height: 1.5;
}

.error {
  color: red;
}
</style>
