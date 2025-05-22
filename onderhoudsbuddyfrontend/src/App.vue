<script setup>
import { ref, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const isLoggedIn = ref(false);

// Controleer inlogstatus bij het laden van de component
onMounted(() => {
  checkLoginStatus();
});

// Update de inlogstatus wanneer nodig
function checkLoginStatus() {
  // Controleer op JWT token
  const token = localStorage.getItem('token'); // of waar je de token opslaat
  isLoggedIn.value = !!token;
}

// Uitlogfunctie
function logout() {
  // Verwijder token
  localStorage.removeItem('token');
  // Eventueel andere opgeslagen gegevens verwijderen
  isLoggedIn.value = false;
  // Navigeer naar de loginpagina
  router.push('/login');
}

// Luister naar veranderingen in de route om de inlogstatus bij te werken
watch(() => router.currentRoute.value, () => {
  checkLoginStatus();
}, { deep: true });
</script>

<template>
  <div class="layout">
    <!-- Sidebar -->
    <aside class="sidebar">
      <div class="sidebar-header">
        <h2 class="logo">OnderhoudsBuddy</h2>
      </div>
      <nav class="sidebar-nav">
        <ul>
          <li>
            <router-link to="/home" class="nav-link">
              <span>Home</span>
            </router-link>
          </li>
          <!-- Toon alleen Login en Registreren als gebruiker NIET is ingelogd -->
          <li v-if="!isLoggedIn">
            <router-link to="/login" class="nav-link">
              <span>Login</span>
            </router-link>
          </li>
          <li v-if="!isLoggedIn">
            <router-link to="/register" class="nav-link">
              <span>Registreren</span>
            </router-link>
          </li>
          <!-- Toon deze opties alleen als gebruiker WEL is ingelogd -->
          <li v-if="isLoggedIn">
            <router-link to="/CarRegister" class="nav-link">
              <span>Auto regristreren</span>
            </router-link>
          </li>
          <li v-if="isLoggedIn">
            <router-link to="/SearchCar" class="nav-link">
              <span>Auto zoeken</span>
            </router-link>
          </li>
          <!-- Voeg een uitlogknop toe als gebruiker is ingelogd -->
          <li v-if="isLoggedIn">
            <a @click="logout" class="nav-link logout-link">
              <span>Uitloggen</span>
            </a>
          </li>
        </ul>
      </nav>
      <div class="sidebar-footer">
        <p>© 2024 Mijn App</p>
      </div>
    </aside>

    <!-- Page Content -->
    <main class="content">
      <router-view v-slot="{ Component }">
        <component :is="Component" class="view-component" />
      </router-view>
    </main>
  </div>
</template>

<style>
/* Fix voor viewportprobleem en zwarte balken */
html, body, #app {
  width: 100%;
  height: 100%;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: #f8f9fa; /* Achtergrondkleur aanpassen naar licht grijs */
}

/* Universele Reset */
*, *::before, *::after {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

#app {
  display: flex;
  width: 100%;
  height: 100%;
  position: absolute; /* Fixeert het element op de volledige viewport */
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

/* Layout Container */
.layout {
  display: flex;
  width: 100%;
  height: 100%;
  position: relative; /* Zorgt dat children op juiste positie zitten */
  overflow: hidden;
}

/* Sidebar */
.sidebar {
  flex: 0 0 20%; /* Sidebar neemt standaard 20% van de breedte in */
  max-width: 350px;
  min-width: 200px;
  height: 100%; /* Sidebar moet de volledige hoogte vullen */
  background: linear-gradient(180deg, #667eea, #764ba2);
  color: white;
  display: flex;
  flex-direction: column;
  box-shadow: 3px 0 10px rgba(0, 0, 0, 0.1);
  overflow-y: auto; /* Scrollen binnen de sidebar als de inhoud te groot is */
  position: relative; /* Zorgt dat sidebar correct wordt geplaatst */
  z-index: 10; /* Zorgt dat sidebar boven content ligt bij overlap */
}

.sidebar-header {
  padding: 20px;
  text-align: center;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

.logo {
  font-size: 1.5rem;
  font-weight: bold;
  color: white;
}

.sidebar-nav {
  flex: 1; /* Nav vult de resterende ruimte van de sidebar */
  padding: 20px;
}

.sidebar-nav ul {
  list-style: none;
}

.nav-link {
  display: flex;
  align-items: center;
  padding: 12px 20px;
  text-decoration: none;
  color: rgba(255, 255, 255, 0.85);
  transition: all 0.3s ease;
  font-size: 1rem;
  border-radius: 0 25px 25px 0;
}

.nav-link:hover {
  background-color: rgba(255, 255, 255, 0.1);
}

.router-link-active {
  background-color: rgba(255, 255, 255, 0.15);
  font-weight: bold;
}

.icon {
  margin-right: 10px;
  font-size: 1.2rem;
}

.sidebar-footer {
  padding: 15px;
  text-align: center;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  font-size: 0.8rem;
  color: rgba(255, 255, 255, 0.7);
}

/* Content Area */
.content {
  flex: 1; /* Content neemt alle resterende ruimte in beslag */
  overflow-y: auto; /* Scrollen binnen het contentgebied indien nodig */
  background-color: #f8f9fa;
  padding: 20px;
  min-width: 0; /* Voorkomt dat flex child buiten zijn container groeit */
  position: relative; /* Zorgt dat content correct wordt geplaatst */
  z-index: 5;
}

/* Router View Component */
.view-component {
  display: flex;
  flex-direction: column;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.05);
  width: 100%;
  height: 100%;
  overflow: hidden; /* Voorkomt dubbele scrollbars */
}

/* Voeg transition toe voor vloeiende responsiviteit */
.sidebar, .content {
  transition: all 0.3s ease-in-out;
}

/* Verwijder zwarte achtergrond van debugging */
body {
  font-family: Arial, sans-serif;
}

/* Fix voor mobiele apparaten en browser compatibiliteit */
@media (max-width: 768px) {
  .layout {
    width: 100vw;
    height: 100vh;
  }
}
</style>

<style>
/* Bestaande stijlen behouden */

/* Extra stijl voor uitloggen link om te laten zien dat het klikbaar is */
.logout-link {
  cursor: pointer;
}

.logout-link:hover {
  background-color: rgba(255, 0, 0, 0.2); /* Subtiele rode hover voor uitloggen */
}
</style>
