import { createRouter, createWebHistory } from "vue-router";
import Login from './views/Login.vue';
import Register from './views/Register.vue';
import Home from './views/Home.vue';
import CarRegister from "@/views/CarRegister.vue";
import SearchCar from "@/views/SearchCar.vue";
import UserCar from "@/views/UserCar.vue";
import CarDetails from "@/views/CarView.vue";
import CarEdit from "@/views/Service.vue";

const routes = [
    { path: '/', component: Login },
    { path: '/login', component: Login },
    { path: '/home', component: Home },
    { path: '/register', component: Register },
    { path: '/CarRegister', component: CarRegister },
    { path: '/SearchCar', component: SearchCar },
    { path: '/UserCar', component: UserCar },
    {
        path: '/car-details/:carId',
        name: 'CarDetails',
        component: CarDetails,
        props: true,
    },
    {
        path: '/car-edit/:carId',
        name: 'CarEdit',
        component: CarEdit,
        props: true,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const token = localStorage.getItem('token');
    const isLoggedIn = !!token;

    console.log('Navigatie naar:', to.path);
    console.log('Ingelogd:', isLoggedIn);
    console.log('Token:', token);

    if ((to.path === '/login' || to.path === '/register' || to.path === '/') && isLoggedIn) {
        console.log('Omleiding naar home');
        next('/home');
    } else {
        console.log('Normale navigatie');
        next();
    }
});

export default router;
