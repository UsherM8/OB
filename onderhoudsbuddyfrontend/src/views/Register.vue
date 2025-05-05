<template>
  <div class="container">
    <h2>Register</h2>
    <form @submit.prevent="handleRegister">
      <input type="text" v-model="name" placeholder="Full Name" required />
      <input type="email" v-model="email" placeholder="Email" required />
      <input type="password" v-model="password" placeholder="Password" required />
      <button type="submit">Register</button>
    </form>
    <p>Already have an account? <router-link to="/">Login</router-link></p>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const name = ref('');
const email = ref('');
const password = ref('');
const router = useRouter();

const handleRegister = () => {
  if (name.value && email.value && password.value) {
    alert(`Registered as ${name.value}`);
    router.push('/');
  } else {
    alert('Please fill in all fields.');
  }
};
</script>

<style scoped>
.container { max-width: 300px; margin: auto; text-align: center; }
input { display: block; width: 100%; margin-bottom: 10px; padding: 8px; }
button { width: 100%; padding: 8px; background: green; color: white; border: none; cursor: pointer; }
</style>
