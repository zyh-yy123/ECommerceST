import { createApp } from 'vue';
import App from './App.vue';
import router from './router';             // 引入上面写好的 router/index.js
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
import './assets/styles/cyberpunk.css';
import '@fortawesome/fontawesome-free/css/all.css';

const app = createApp(App);

// 注册路由
app.use(router);

// 注册 Element Plus 组件库
app.use(ElementPlus);

app.mount('#app');


