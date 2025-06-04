<template>
  <div class="register-container">
    <div class="cyber-background"></div>
    <el-card class="register-card">
      <template #header>
        <h2 class="cyber-title">用户注册</h2>
      </template>
      
      <el-form :model="registerForm" :rules="rules" ref="registerFormRef" label-width="100px" class="cyber-form">
        <el-form-item label="用户名" prop="username">
          <el-input v-model="registerForm.username" placeholder="请输入用户名" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item label="密码" prop="password">
          <el-input v-model="registerForm.password" type="password" placeholder="请输入密码" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item label="确认密码" prop="confirmPassword">
          <el-input v-model="registerForm.confirmPassword" type="password" placeholder="请再次输入密码" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="registerForm.email" placeholder="请输入邮箱" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item label="手机号码" prop="phoneNumber">
          <el-input v-model="registerForm.phoneNumber" placeholder="请输入手机号码" class="cyber-input"></el-input>
        </el-form-item>
        
        <el-form-item>
          <el-button type="primary" @click="handleRegister" :loading="loading" class="cyber-button">注册</el-button>
          <el-button @click="$router.push('/login')" class="cyber-button-secondary">返回登录</el-button>
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
  name: 'RegisterView',
  setup() {
    const router = useRouter();
    const registerFormRef = ref(null);
    const loading = ref(false);

    const registerForm = reactive({
      username: '',
      password: '',
      confirmPassword: '',
      email: '',
      phoneNumber: ''
    });

    const validatePass = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请输入密码'));
      } else {
        if (registerForm.confirmPassword !== '') {
          registerFormRef.value?.validateField('confirmPassword');
        }
        callback();
      }
    };

    const validatePass2 = (rule, value, callback) => {
      if (value === '') {
        callback(new Error('请再次输入密码'));
      } else if (value !== registerForm.password) {
        callback(new Error('两次输入密码不一致'));
      } else {
        callback();
      }
    };

    const rules = {
      username: [
        { required: true, message: '请输入用户名', trigger: 'blur' },
        { min: 3, max: 20, message: '长度在 3 到 20 个字符', trigger: 'blur' }
      ],
      password: [
        { required: true, validator: validatePass, trigger: 'blur' },
        { min: 6, message: '密码长度不能小于6位', trigger: 'blur' }
      ],
      confirmPassword: [
        { required: true, validator: validatePass2, trigger: 'blur' }
      ],
      email: [
        { required: true, message: '请输入邮箱地址', trigger: 'blur' },
        { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' }
      ],
      phoneNumber: [
        { required: true, message: '请输入手机号码', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
      ]
    };

    const handleRegister = async () => {
      if (!registerFormRef.value) return;

      try {
        const valid = await registerFormRef.value.validate();
        if (!valid) return;

        loading.value = true;
        const response = await axios.post('/api/Users/register', {
          username: registerForm.username,
          password: registerForm.password,
          email: registerForm.email,
          phoneNumber: registerForm.phoneNumber
        });

        ElMessage.success('注册成功');
        router.push('/login');
      } catch (error) {
        if (error.response?.data?.message) {
          ElMessage.error(error.response.data.message);
        } else {
          ElMessage.error('注册失败，请稍后重试');
        }
      } finally {
        loading.value = false;
      }
    };

    return {
      registerForm,
      registerFormRef,
      rules,
      loading,
      handleRegister
    };
  }
};
</script>

<style scoped>
.register-container {
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

.register-card {
  width: 500px;
  background: rgba(20, 20, 20, 0.9);
  border: 1px solid #00ff9d;
  box-shadow: 0 0 20px rgba(0, 255, 157, 0.2);
  backdrop-filter: blur(10px);
}

.register-card :deep(.el-card__header) {
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
