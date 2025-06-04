<template>
  <div class="login-container">
    <div class="cyber-card">
      <h1 class="cyber-title">登录</h1>
      <form @submit.prevent="handleLogin">
        <div class="form-group">
          <label>用户名</label>
          <input 
            type="text" 
            v-model="username" 
            class="cyber-input" 
            required
            placeholder="输入用户名"
          >
        </div>
        <div class="form-group">
          <label>密码</label>
          <input 
            type="password" 
            v-model="password" 
            class="cyber-input" 
            required
            placeholder="输入密码"
          >
        </div>
        <div v-if="error" class="cyber-alert">
          {{ error }}
        </div>
        <button type="submit" class="cyber-button">登录</button>
      </form>
      <p class="register-link">
        还没有账号？ <router-link to="/register">立即注册</router-link>
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'LoginPage',
  data() {
    return {
      username: '',
      password: '',
      error: ''
    }
  },
  methods: {
    async handleLogin() {
      try {
        // 实现登录逻辑
        this.error = '';
        // TODO: 调用登录 API
      } catch (err) {
        this.error = '登录失败，请检查用户名和密码';
      }
    }
  }
}
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: calc(100vh - 100px);
}

.cyber-card {
  width: 100%;
  max-width: 400px;
  padding: 30px;
}

.form-group {
  margin-bottom: 20px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
  color: var(--neon-blue);
  text-transform: uppercase;
  letter-spacing: 1px;
}

.cyber-input {
  width: 100%;
  margin-bottom: 10px;
}

.register-link {
  margin-top: 20px;
  text-align: center;
  color: var(--text-secondary);
}

.register-link a {
  color: var(--neon-pink);
  text-decoration: none;
  transition: all 0.3s;
}

.register-link a:hover {
  text-shadow: 0 0 10px var(--neon-pink);
}
</style> 