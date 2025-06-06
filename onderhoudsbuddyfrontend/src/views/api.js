import axios from 'axios';

axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

axios.interceptors.response.use(
  response => response,
  error => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('token');
      router.push('/login');
    }
    return Promise.reject(error);
  }
);
const apiClientDotnet = axios.create({
    baseURL: 'https://localhost:7259',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,

});

const apiClientJava = axios.create({
    baseURL: 'http://localhost:8080',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,

});

export default {
    // Auto endpoints
    getCarById(id) {
        return apiClientDotnet.get(`/api/Car/by-id/${id}`);
    },

    getCarByLicense(licensePlate) {
        return apiClientDotnet.get(`/api/Car/by-license/${licensePlate}`);
    },

    getCarDetails(id) {
        return apiClientDotnet.get(`/api/Car/details/${id}`);
    },

    addCar(carData, userId) {
        return apiClientDotnet.post(`/api/Car/user/${userId}`, carData);
    },

    updateCar(licensePlate, carData) {
        return apiClientDotnet.put(`/api/Car/${licensePlate}`, carData);
    },

    deleteCar(id) {
        return apiClientDotnet.delete(`np`);
    },

    getUserCars(userId) {
        return apiClientDotnet.get(`/api/Car/user/${userId}/cars`);
    },

    getUserCar(userId, carId) {
        return apiClientDotnet.get(`/api/Car/user/${userId}/car/${carId}`);
    },

    // User endpoints
    getAllUsers() {
        return apiClientJava.get('/api/users');
    },

    getUserById(id) {
        return apiClientJava.get(`/api/users/${id}`);
    },

    createUser(userData) {
        return apiClientJava.post('/api/users', userData);
    },

    updateUser(id, userData) {
        return apiClientJava.put(`/api/users/${id}`, userData);
    },

    deleteUser(id) {
        return apiClientJava.delete(`/api/users/${id}`);
    },

    // Service endpoints (.NET backend)
    getServiceById(id) {
        return apiClientDotnet.get(`/api/Service/by-id/${id}`);
    },

    getAllServicesById(id) {
        return apiClientDotnet.get(`/api/Service/Getallby-id/${id}`);
    },

    createService(serviceData) {
        return apiClientDotnet.post('/api/Service', serviceData);
    },

    // Garage endpoints (.NET backend)
    getGarageById(id) {
        return apiClientDotnet.get(`/api/Garage/by-id/${id}`);
    },

    getAllGarages() {
        return apiClientDotnet.get(`/api/Garage`);
    },

    createGarage(garageData) {
        return apiClientDotnet.post(`/api/Garage`, garageData);
    },

    // Authentication endpoints
    login(email, password) {
        return apiClientJava.post('/api/auth/login', { email, password });
    },

    logout() {
        return apiClientJava.post('/api/auth/logout');
    },

    setAuthToken(token) {
        apiClientJava.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    },

    clearAuthToken() {
        delete apiClientJava.defaults.headers.common['Authorization'];
    }
};

