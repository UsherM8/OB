import {createRouter, createWebHistory} from "vue-router";
import Login from './views/Login.vue';
import Register from './views/Register.vue';
import Home from './views/Home.vue';
import CarRegister from "@/views/CarRegister.vue";
import SearchCar from "@/views/SearchCar.vue";

const routes = [
    {path: '/', component: Login},
    {path: '/login', component: Login},
    {path: '/home', component: Home},
    {path: '/register', component: Register},
    {path: '/CarRegister', component: CarRegister},
    {path: '/SearchCar', component: SearchCar},
];

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router
