<template>
  <div class="register-container">
    <div class="cyber-card">
      <h1 class="cyber-title">注册</h1>
      <form @submit.prevent="handleRegister">
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
          <label>邮箱</label>
          <input 
            type="email" 
            v-model="email" 
            class="cyber-input" 
            required
            placeholder="输入邮箱"
          >
        </div>
        <div class="form-group">
          <label>手机号</label>
          <input 
            type="tel" 
            v-model="phoneNumber" 
            class="cyber-input" 
            required
            placeholder="输入手机号"
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
        <div class="form-group">
          <label>确认密码</label>
          <input 
            type="password" 
            v-model="confirmPassword" 
            class="cyber-input" 
            required
            placeholder="再次输入密码"
          >
        </div>
        <div v-if="error" class="cyber-alert">
          {{ error }}
        </div>
        <button type="submit" class="cyber-button">注册</button>
      </form>
      <p class="login-link">
        已有账号？ <router-link to="/login">立即登录</router-link>
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'RegisterPage',
  data() {
    return {
      username: '',
      email: '',
      phoneNumber: '',
      password: '',
      confirmPassword: '',
      error: ''
    }
  },
  methods: {
    async handleRegister() {
      try {
        if (this.password !== this.confirmPassword) {
          this.error = '两次输入的密码不一致';
          return;
        }

        this.error = '';
        // TODO: 调用注册 API
        const response = await fetch('/api/Users/register', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            username: this.username,
            email: this.email,
            phoneNumber: this.phoneNumber,
            password: this.password
          })
        });

        if (!response.ok) {
          const data = await response.json();
          throw new Error(data.message || '注册失败');
        }

        this.$router.push('/login');
      } catch (err) {
        this.error = err.message || '注册失败，请稍后重试';
      }
    }
  }
}
</script>

<style scoped>
.register-container {
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

.login-link {
  margin-top: 20px;
  text-align: center;
  color: var(--text-secondary);
}

.login-link a {
  color: var(--neon-pink);
  text-decoration: none;
  transition: all 0.3s;
}

.login-link a:hover {
  text-shadow: 0 0 10px var(--neon-pink);
}
</style> 