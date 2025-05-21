import axios from 'axios';


const apiClientDotnet = axios.create({
    baseURL: 'https://localhost:44393',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,

});

const apiClientJava = axios.create({
    baseURL: 'https://localhost:8080/api',
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

    addCar(carData) {
        return apiClientDotnet.post('/api/Car', carData);
    },

    updateCar(licensePlate, carData) {
        return apiClientDotnet.put(`/api/Car/${licensePlate}`, carData);
    },

    deleteCar(id) {
        return apiClientDotnet.delete(`/api/Car/${id}`);
    }
    // User endpoints
    getAllUsers() {
        return apiClientJava.get('/users');
    },

    getUserById(id) {
        return apiClientJava.get(`/users/${id}`);
    },

    createUser(userData) {
        return apiClientJava.post('/users', userData);
    },

    updateUser(id, userData) {
        return apiClientJava.put(`/users/${id}`, userData);
    },

    deleteUser(id) {
        return apiClientJava.delete(`/users/${id}`);
    },

    // Authentication endpoints
    login(email, password) {
        return apiClientJava.post('/auth/login', { email, password });
    },

    logout() {
        return apiClientJava.post('/auth/logout');
    },
    
    setAuthToken(token) {
        apiClientJava.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    },

    // Helper functie voor het verwijderen van de auth token
    clearAuthToken() {
        delete apiClientJava.defaults.headers.common['Authorization'];
    }

};
