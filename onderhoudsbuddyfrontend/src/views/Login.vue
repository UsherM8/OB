<template>
  <div class="container">
    <h2>Login</h2>
    <form @submit.prevent="handleLogin">
      <input type="email" v-model="email" placeholder="Email" required />
      <input type="password" v-model="password" placeholder="Password" required />
      <button type="submit">Login</button>
    </form>
    <p>Don't have an account? <router-link to="/register">Register</router-link></p>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const email = ref('');
const password = ref('');
const router = useRouter();

const handleLogin = () => {
  if (email.value && password.value) {
    alert(`Logged in as ${email.value}`);
    router.push('/home');
  } else {
    alert('Please fill in all fields.');
  }
};
</script>

<style scoped>
.container { max-width: 300px; margin: auto; text-align: center; }
input { display: block; width: 100%; margin-bottom: 10px; padding: 8px; }
button { width: 100%; padding: 8px; background: blue; color: white; border: none; cursor: pointer; }
</style>
