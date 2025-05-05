import {createRouter, createWebHistory} from "vue-router";
import Login from './views/Login.vue';
import Register from './views/Register.vue';
import Home from './views/Home.vue';

const routes = [
    {path: '/', component: Login},
    {path: '/login', component: Login},
    {path: '/home', component: Home},
    {path: '/register', component: Register},
];

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router
