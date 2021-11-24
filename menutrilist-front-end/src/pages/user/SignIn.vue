<template>
  <q-page class="column items-left q-mx-lg">
    <div class="login-card column items-left q-mt-xl">
      <h3 class="text-bold q-mt-sm">Sign In</h3>
      <form @submit.prevent="signIn" class="row justify-start q-gutter-x-lg">
        <BaseInput
          class="login-input col-xs-11 col-sm-10"
          v-model="userName"
          ref="userName"
          lazy-rules
        ><b>Username</b>
        </BaseInput>

        <BaseInput
          class="login-input col-xs-11 col-sm-10"
          v-model="password"
          ref="password"
          password
          lazy-rules
        ><b>Password</b>
        </BaseInput>

        <base-button
          class="login-button col-3"
          type="submit"
        >
          Sign in
        </base-button>
      </form>
      <router-link
        class="login-link"
        to="/sign-up"
      >Create Account
      </router-link>
      <router-link
        class="login-link"
        to="/forgot-password"
      >Forgot Password
      </router-link>
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseButton from 'components/ui/BaseButton.vue';
import BaseInput from 'components/ui/BaseInput.vue';
// import { EUserActions } from 'src/store/user/types/enumeration';

export default defineComponent({
  components: {
    BaseButton,
    BaseInput,
  },
  data() {
    return {
      userName: '' as string,
      password: '' as string,
    };
  },
//   methods: {
//     async signIn() {
//       if (!this.userName || !this.password) {
//         this.$q.notify({ message: 'Please enter valid data' })
//         return;
//       }
//       if (
//         !await this.$store.dispatch(EUserActions.login, {
//           userName: this.userName,
//           password: this.password,
//         })
//       ) {
//         this.$q.notify({
//           message: 'Incorrect username or password..',
//         });
//       } else {
//         await this.$router.replace('/');
//       }
//     },
//   },
//   watch: {
//     async isAuth(value: boolean) {
//       if (value) {
//         await this.$router.push('/');
//       } else {
//         await this.$router.push('/login');
//       }
//     },
//   },
});
</script>

<style
  lang="scss"
  scoped
>
.login-input {
  font-size: 15px;
  font-weight: 700;
  margin-top: 10px;
}

.login-button {
  margin-top: 40px;
  margin-bottom: 30px;
  font-size: 20px;
  font-weight: 700;
  padding: 0.1rem;
  cursor: pointer;
  border-radius: 15px;
}
</style>
