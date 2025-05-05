import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router.js'

// create the app instance
const app = createApp(App)

// use router and mount
app.use(router)
app.mount('#app')
