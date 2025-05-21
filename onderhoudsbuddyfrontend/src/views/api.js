import axios from 'axios';


const apiClient = axios.create({
    baseURL: 'https://localhost:44393',
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 10000,

});

export default {
    // Auto endpoints
    getCarById(id) {
        return apiClient.get(`/api/Car/by-id/${id}`);
    },

    getCarByLicense(licensePlate) {
        return apiClient.get(`/api/Car/by-license/${licensePlate}`);
    },

    getCarDetails(id) {
        return apiClient.get(`/api/Car/details/${id}`);
    },

    addCar(carData) {
        return apiClient.post('/api/Car', carData);
    },

    updateCar(licensePlate, carData) {
        return apiClient.put(`/api/Car/${licensePlate}`, carData);
    },

    deleteCar(id) {
        return apiClient.delete(`/api/Car/${id}`);
    }
    // user endpoints
};
