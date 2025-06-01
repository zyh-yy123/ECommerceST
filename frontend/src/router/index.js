import { createRouter, createWebHistory } from 'vue-router';

// **先引入我们刚才创建好的多单词命名的组件**
const HomeView = () => import('../views/HomeView.vue');
const LoginView = () => import('../views/LoginView.vue');
const RegisterView = () => import('../views/RegisterView.vue');
const ProductListView = () => import('../views/ProductListView.vue');
const ProductDetailView = () => import('../views/ProductDetailView.vue');
const CartView = () => import('../views/CartView.vue');
const CheckoutView = () => import('../views/CheckoutView.vue');
const OrderHistoryView = () => import('../views/OrderHistoryView.vue');

const routes = [
  // 根路径重定向到 /home
  {
    path: '/',
    redirect: '/home'
  },
  {
    path: '/home',
    name: 'HomeView',           // **name 可以和组件内部的 name 一致，也可以不用精确匹配，但建议一致**
    component: HomeView
  },
  {
    path: '/login',
    name: 'LoginView',
    component: LoginView
  },
  {
    path: '/register',
    name: 'RegisterView',
    component: RegisterView
  },
  {
    path: '/products',
    name: 'ProductListView',
    component: ProductListView
  },
  {
    path: '/product/:id',
    name: 'ProductDetailView',
    component: ProductDetailView,
    props: true
  },
  {
    path: '/cart',
    name: 'CartView',
    component: CartView
  },
  {
    path: '/checkout',
    name: 'CheckoutView',
    component: CheckoutView
  },
  {
    path: '/orders',
    name: 'OrderHistoryView',
    component: OrderHistoryView
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;

