<template>
  <b-modal v-model="isActive" :can-cancel="false">
    <div class="card">
      <div class="card-content">
        <b-field label="Логин">
          <b-input v-model="login"></b-input>
        </b-field>
        <b-field label="Пароль">
          <b-input v-model="password"></b-input>
        </b-field>
        <b-button @click="getToken">Войти</b-button>
      </div>
    </div>
  </b-modal>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
import {BButton} from 'buefy/dist/esm/button'
import {BModal} from 'buefy/dist/esm/modal'
import {BField} from 'buefy/dist/esm/field'
import {BInput} from 'buefy/dist/esm/input'
import {AuthService} from "@/services/AuthService";
import {RequestHelper} from "@/services/RequestHelper";

@Component({
  components: {'b-modal': BModal, 'b-button': BButton, 'b-field': BField, 'b-input': BInput}
})
export default class Login extends Vue {
  login = ""
  password = ""
  isActive = true

  async created() {
    const tokenValid = await AuthService.isTokenValid()
    if (tokenValid) {
      this.exit()
    }
  }

  exit() {
    this.isActive = false
    this.$emit('exit')
  }

  async getToken() {
    AuthService.saveCredentials(this.login, this.password)
    if (await AuthService.isTokenValid()) {
      this.exit()
    }
  }
}
</script>

<style scoped>

</style>