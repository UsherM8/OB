<template>
  <div class="layout">
    <!-- Sidebar -->
    <aside class="sidebar">
      <h2>Menu</h2>
      <ul>
        <li><router-link to="/home">Home</router-link></li>
        <li><router-link to="/login">Login</router-link></li>
        <li><router-link to="/register">Register</router-link></li>
      </ul>
    </aside>
    <!-- Page Content -->
    <main class="content">
      <router-view />
    </main>
  </div>
</template>

<style >
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

.layout {
  display: flex;
  min-height: 100vh;
}

.sidebar {
  width: 200px;
  background-color: #8d90ea;
  color: #fff;
  padding: 20px;
}

.sidebar a {
  color: #fff;
  text-decoration: none;
  display: block;
  margin-bottom: 10px;
}

.content {
  flex-grow: 1;
  padding: 20px;
}
</style>
<script setup lang="ts">
</script>
