<template>
  <div class="home-container">
    <div class="home-content">
      <h2>Welkom op de Home Pagina</h2>
      <p>Je bent ingelogd!</p>
      <router-link to="/" class="logout-link">Uitloggen</router-link>
    </div>
  </div>
</template>

<style scoped>
.home-container {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.home-content {
  background-color: white;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  text-align: center;
  max-width: 500px;
  width: 90%;
}

h2 {
  color: #333;
  margin-bottom: 15px;
  font-size: 1.8rem;
}

p {
  margin-bottom: 20px;
  color: #555;
  font-size: 1.1rem;
}

.logout-link {
  display: inline-block;
  background: linear-gradient(90deg, #667eea, #764ba2);
  color: white;
  padding: 10px 20px;
  border-radius: 25px;
  text-decoration: none;
  font-weight: bold;
  transition: all 0.3s ease;
}

.logout-link:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}
</style>
