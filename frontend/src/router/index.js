import { createRouter, createWebHistory } from 'vue-router';
import Register from '../views/Register.vue';
import Products from '../views/Products.vue';
import Cart from '../views/Cart.vue';
import Orders from '../views/Orders.vue';

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
  // 根路径重定向到 /products
  {
    path: '/',
    redirect: '/products'
  },
  {
    path: '/home',
    name: 'HomeView',
    component: HomeView
  },
  {
    path: '/login',
    name: 'LoginView',
    component: LoginView,
    meta: { requiresAuth: false }
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
    meta: { requiresAuth: false }
  },
  {
    path: '/products',
    name: 'Products',
    component: Products,
    meta: { requiresAuth: true }
  },
  {
    path: '/product/:id',
    name: 'ProductDetailView',
    component: ProductDetailView,
    props: true
  },
  {
    path: '/cart',
    name: 'Cart',
    component: Cart,
    meta: { requiresAuth: true }
  },
  {
    path: '/checkout',
    name: 'CheckoutView',
    component: CheckoutView
  },
  {
    path: '/checkout/:orderId',
    name: 'Checkout',
    component: () => import('../views/Checkout.vue'),
    meta: { requiresAuth: true }
  },
  {
    path: '/orders',
    name: 'Orders',
    component: Orders,
    meta: { requiresAuth: true }
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

// 路由守卫
router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem('token');
  
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login');
  } else if (!to.meta.requiresAuth && isAuthenticated) {
    next('/products');
  } else {
    next();
  }
});

export default router;

