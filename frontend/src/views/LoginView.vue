<template>
  <div class="login-container">
    <div class="cyber-background"></div>
    <el-card class="login-card">
      <template #header>
        <h2 class="cyber-title">用户登录</h2>
      </template>
      
      <el-form :model="loginForm" :rules="rules" ref="loginFormRef" label-width="80px" class="cyber-form">
        <el-form-item label="用户名" prop="username">
          <el-input v-model="loginForm.username" placeholder="请输入用户名" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item label="密码" prop="password">
          <el-input v-model="loginForm.password" type="password" placeholder="请输入密码" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item>
          <el-button type="primary" @click="handleLogin" :loading="loading" class="cyber-button">登录</el-button>
          <el-button @click="$router.push('/register')" class="cyber-button-secondary">注册账号</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive } from 'vue';
import { useRouter } from 'vue-router';
import { ElMessage } from 'element-plus';
import axios from 'axios';

export default {
  name: 'LoginView',
  setup() {
    const router = useRouter();
    const loginFormRef = ref(null);
    const loading = ref(false);

    const loginForm = reactive({
      username: '',
      password: ''
    });

    const rules = {
      username: [
        { required: true, message: '请输入用户名', trigger: 'blur' }
      ],
      password: [
        { required: true, message: '请输入密码', trigger: 'blur' }
      ]
    };

    const handleLogin = async () => {
      console.log('Login button clicked');
      
      if (!loginFormRef.value) {
        console.error('Form reference is not available');
        return;
      }

      try {
        const valid = await loginFormRef.value.validate();
        console.log('Form validation result:', valid);
        
        if (!valid) {
          console.log('Form validation failed');
          return;
        }

        loading.value = true;
        console.log('Sending login request...');
        
        const response = await axios.post('/api/Users/login', {
          username: loginForm.username,
          password: loginForm.password
        });

        console.log('Login response:', response.data);

        if (response.data.success) {
          localStorage.setItem('user', JSON.stringify(response.data.data));
          localStorage.setItem('token', response.data.data.token);
          
          ElMessage.success('登录成功');
          router.push('/home');
        } else {
          ElMessage.error(response.data.message || '登录失败');
        }
      } catch (error) {
        console.error('Login error:', error);
        if (error.response) {
          console.error('Error response:', error.response.data);
          ElMessage.error(error.response.data.message || '登录失败，请检查用户名和密码');
        } else if (error.request) {
          console.error('No response received:', error.request);
          ElMessage.error('网络错误，请稍后重试');
        } else {
          console.error('Error message:', error.message);
          ElMessage.error('登录失败，请稍后重试');
        }
      } finally {
        loading.value = false;
      }
    };

    return {
      loginForm,
      loginFormRef,
      rules,
      loading,
      handleLogin
    };
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #0a0a0a;
  position: relative;
  overflow: hidden;
}

.cyber-background {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: 
    linear-gradient(45deg, #0a0a0a 25%, transparent 25%),
    linear-gradient(-45deg, #0a0a0a 25%, transparent 25%),
    linear-gradient(45deg, transparent 75%, #0a0a0a 75%),
    linear-gradient(-45deg, transparent 75%, #0a0a0a 75%);
  background-size: 20px 20px;
  background-position: 0 0, 0 10px, 10px -10px, -10px 0px;
  opacity: 0.1;
}

.login-card {
  width: 400px;
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid #00ff9d;
  box-shadow: 0 0 20px rgba(0, 255, 157, 0.2);
  backdrop-filter: blur(10px);
}

.login-card :deep(.el-card__header) {
  text-align: center;
  border-bottom: 1px solid #00ff9d;
  padding: 20px;
}

.cyber-title {
  margin: 0;
  color: #00ff9d;
  font-size: 24px;
  text-transform: uppercase;
  letter-spacing: 2px;
  text-shadow: 0 0 10px rgba(0, 255, 157, 0.5);
}

.cyber-form {
  padding: 20px;
}

.cyber-form :deep(.el-form-item__label) {
  color: #00ff9d;
}

.cyber-input :deep(.el-input__inner) {
  background-color: rgba(0, 0, 0, 0.5);
  border: 1px solid #00ff9d;
  color: #fff;
}

.cyber-input :deep(.el-input__inner:focus) {
  border-color: #00ff9d;
  box-shadow: 0 0 10px rgba(0, 255, 157, 0.3);
}

.cyber-button {
  background: linear-gradient(45deg, #00ff9d, #00b8ff);
  border: none;
  color: #000;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  transition: all 0.3s ease;
}

.cyber-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.5);
}

.cyber-button-secondary {
  background: transparent;
  border: 1px solid #00ff9d;
  color: #00ff9d;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  transition: all 0.3s ease;
}

.cyber-button-secondary:hover {
  background: rgba(0, 255, 157, 0.1);
  transform: translateY(-2px);
  box-shadow: 0 0 15px rgba(0, 255, 157, 0.3);
}

:deep(.el-message) {
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid #00ff9d;
  color: #00ff9d;
}
</style>
