<template>
  <div id="app">
    <!-- 左侧导航栏 -->
    <el-menu
      mode="vertical"
      background-color="#0a0a0a"
      text-color="#00ff9d"
      active-text-color="#00b8ff"
      class="cyber-menu"
      :default-active="activeIndex"
    >
      <div class="menu-header">
        <h2 class="cyber-logo">ZW商城</h2>
      </div>
      
      <el-menu-item index="1">
        <router-link to="/home" class="cyber-link">
          <i class="el-icon-s-home"></i>
          <span>首页</span>
        </router-link>
      </el-menu-item>

      <el-sub-menu index="2">
        <template #title>
          <i class="el-icon-s-goods"></i>
          <span>商品管理</span>
        </template>
        <el-menu-item index="2-1">
          <router-link to="/products" class="cyber-link">
            <i class="el-icon-s-grid"></i>
            <span>商品列表</span>
          </router-link>
        </el-menu-item>
        <el-menu-item index="2-2">
          <router-link to="/products/categories" class="cyber-link">
            <i class="el-icon-menu"></i>
            <span>商品分类</span>
          </router-link>
        </el-menu-item>
      </el-sub-menu>

      <el-sub-menu index="3">
        <template #title>
          <i class="el-icon-shopping-cart-2"></i>
          <span>购物管理</span>
        </template>
        <el-menu-item index="3-1">
          <router-link to="/cart" class="cyber-link">
            <i class="el-icon-shopping-cart-full"></i>
            <span>购物车</span>
          </router-link>
        </el-menu-item>
        <el-menu-item index="3-2">
          <router-link to="/wishlist" class="cyber-link">
            <i class="el-icon-star-on"></i>
            <span>收藏夹</span>
          </router-link>
        </el-menu-item>
      </el-sub-menu>

      <el-sub-menu index="4">
        <template #title>
          <i class="el-icon-s-order"></i>
          <span>订单管理</span>
        </template>
        <el-menu-item index="4-1">
          <router-link to="/orders" class="cyber-link">
            <i class="el-icon-document"></i>
            <span>订单历史</span>
          </router-link>
        </el-menu-item>
        <el-menu-item index="4-2">
          <router-link to="/orders/processing" class="cyber-link">
            <i class="el-icon-time"></i>
            <span>处理中订单</span>
          </router-link>
        </el-menu-item>
        <el-menu-item index="4-3">
          <router-link to="/orders/completed" class="cyber-link">
            <i class="el-icon-circle-check"></i>
            <span>已完成订单</span>
          </router-link>
        </el-menu-item>
      </el-sub-menu>

      <div class="menu-footer">
        <template v-if="user">
          <div class="user-info">
            <span class="username">{{ user.username }}</span>
            <el-button type="text" @click="handleLogout" class="cyber-button-text">退出</el-button>
          </div>
        </template>
        <template v-else>
          <div class="auth-buttons">
            <router-link to="/login" class="cyber-link">登录</router-link>
            <router-link to="/register" class="cyber-link">注册</router-link>
          </div>
        </template>
      </div>
    </el-menu>

    <!-- 主内容区域 -->
    <div class="main-content">
      <router-view />
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';

export default {
  name: 'App',
  setup() {
    const router = useRouter();
    const route = useRoute();
    const user = ref(null);
    const activeIndex = ref('1');

    const loadUser = () => {
      try {
        const userStr = localStorage.getItem('user');
        if (userStr) {
          user.value = JSON.parse(userStr);
        }
      } catch (error) {
        console.error('Error loading user:', error);
        user.value = null;
      }
    };

    const handleLogout = () => {
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      user.value = null;
      router.push('/login');
    };

    onMounted(() => {
      loadUser();
      // 根据当前路由设置活动菜单项
      const path = route.path;
      if (path.includes('products')) {
        activeIndex.value = '2-1';
      } else if (path.includes('cart')) {
        activeIndex.value = '3-1';
      } else if (path.includes('orders')) {
        activeIndex.value = '4-1';
      } else {
        activeIndex.value = '1';
      }
    });

    return { user, handleLogout, activeIndex };
  }
};
</script>

<style>
/* 全局样式 */
:root {
  --primary-color: #00ff9d;
  --secondary-color: #00b8ff;
  --background-color: #0a0a0a;
  --text-color: #ffffff;
  --border-color: #00ff9d;
  --menu-width: 240px;
}

body {
  margin: 0;
  padding: 0;
  background-color: var(--background-color);
  color: var(--text-color);
  font-family: 'Arial', sans-serif;
}

#app {
  min-height: 100vh;
  display: flex;
}

/* 导航菜单样式 */
.cyber-menu {
  width: var(--menu-width);
  height: 100vh;
  position: fixed;
  left: 0;
  top: 0;
  border-right: 1px solid var(--border-color);
  box-shadow: 0 0 20px rgba(0, 255, 157, 0.1);
  display: flex;
  flex-direction: column;
}

.menu-header {
  padding: 20px;
  text-align: center;
  border-bottom: 1px solid var(--border-color);
}

.cyber-logo {
  color: var(--primary-color);
  margin: 0;
  font-size: 24px;
  text-transform: uppercase;
  letter-spacing: 2px;
  text-shadow: 0 0 10px rgba(0, 255, 157, 0.5);
}

.cyber-menu :deep(.el-menu-item),
.cyber-menu :deep(.el-sub-menu__title) {
  height: 50px;
  line-height: 50px;
  font-size: 14px;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  display: flex;
  align-items: center;
}

.cyber-menu :deep(.el-menu-item.is-active) {
  background: linear-gradient(45deg, rgba(0, 255, 157, 0.1), rgba(0, 184, 255, 0.1));
  border-right: 2px solid var(--primary-color);
}

.cyber-menu :deep(.el-sub-menu__title:hover),
.cyber-menu :deep(.el-menu-item:hover) {
  background: rgba(0, 255, 157, 0.05);
}

.cyber-menu :deep(.el-sub-menu__title) {
  color: var(--primary-color);
}

.cyber-menu :deep(.el-menu-item) {
  padding-left: 48px !important;
}

.cyber-link {
  color: inherit;
  text-decoration: none;
  transition: all 0.3s ease;
  display: flex;
  align-items: center;
  width: 100%;
}

.cyber-link i {
  margin-right: 10px;
  font-size: 18px;
}

.cyber-link:hover {
  color: var(--secondary-color);
  text-shadow: 0 0 10px rgba(0, 184, 255, 0.5);
}

.menu-footer {
  margin-top: auto;
  padding: 20px;
  border-top: 1px solid var(--border-color);
}

.user-info {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
}

.auth-buttons {
  display: flex;
  flex-direction: column;
  gap: 10px;
  align-items: center;
}

.username {
  color: var(--primary-color);
  font-weight: bold;
}

.cyber-button-text {
  color: var(--primary-color) !important;
  border: 1px solid var(--primary-color);
  padding: 5px 15px;
  border-radius: 4px;
  transition: all 0.3s ease;
}

.cyber-button-text:hover {
  background: rgba(0, 255, 157, 0.1);
  transform: translateY(-2px);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.3);
}

/* 主内容区域样式 */
.main-content {
  flex: 1;
  margin-left: var(--menu-width);
  padding: 20px;
  background-color: var(--background-color);
  min-height: 100vh;
}

/* Element Plus 组件样式覆盖 */
:deep(.el-card) {
  background-color: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  color: var(--text-color);
}

:deep(.el-input__inner) {
  background-color: rgba(0, 0, 0, 0.5);
  border: 1px solid var(--border-color);
  color: var(--text-color);
}

:deep(.el-input__inner:focus) {
  border-color: var(--primary-color);
  box-shadow: 0 0 10px rgba(0, 255, 157, 0.3);
}

:deep(.el-button--primary) {
  background: linear-gradient(45deg, var(--primary-color), var(--secondary-color));
  border: none;
  color: #000;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
}

:deep(.el-button--primary:hover) {
  transform: translateY(-2px);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.5);
}

:deep(.el-message) {
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid var(--border-color);
  color: var(--primary-color);
}
</style>

