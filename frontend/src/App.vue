<template>
  <div id="app">
    <!-- 顶部导航栏（使用 Element Plus 的 Menu 组件）-->
    <el-menu mode="horizontal" background-color="#545c64" text-color="#fff" active-text-color="#ffd04b">
      <el-menu-item index="1">
        <router-link to="/home" style="color: inherit; text-decoration: none;">首页</router-link>
      </el-menu-item>
      <el-menu-item index="2">
        <router-link to="/products" style="color: inherit; text-decoration: none;">商品列表</router-link>
      </el-menu-item>
      <el-menu-item index="3">
        <router-link to="/cart" style="color: inherit; text-decoration: none;">购物车</router-link>
      </el-menu-item>
      <el-menu-item index="4">
        <router-link to="/orders" style="color: inherit; text-decoration: none;">我的订单</router-link>
      </el-menu-item>
      <el-menu-item index="5" style="float: right;">
        <!-- 如果已经登录，可以显示用户名；否则显示登录注册链接 -->
        <template v-if="user">
          {{ user.username }}
          <el-button type="text" @click="handleLogout" style="color: #fff; margin-left: 8px;">退出</el-button>
        </template>
        <template v-else>
          <router-link to="/login" style="color: inherit; margin-right: 10px; text-decoration: none;">登录</router-link>
          <router-link to="/register" style="color: inherit; text-decoration: none;">注册</router-link>
        </template>
      </el-menu-item>
    </el-menu>

    <!-- 路由出口：匹配到哪个路径，就渲染哪个组件 -->
    <router-view />
  </div>
</template>

<script>
import { ref } from 'vue';

export default {
  name: 'App',
  setup() {
    // 从 localStorage 读取是否有登录用户
    const user = ref(null);
    try {
      user.value = JSON.parse(localStorage.getItem('user') || 'null');
    } catch {
      user.value = null;
    }

    const handleLogout = () => {
      localStorage.removeItem('user');
      window.location.href = '/home';
    };

    return { user, handleLogout };
  }
};
</script>

<style>
/* 给 <router-view> 默认间距 */
#app {
  min-height: 100vh;
  background-color: #f5f5f5;
}
</style>

