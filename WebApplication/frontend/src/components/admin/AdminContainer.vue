<template>
  <div>
    <Login v-if="!logined" @exit="logined = true"></Login>
    <AdminScreen v-else></AdminScreen>
  </div>
</template>

<script lang="ts">
import {Component, Vue} from "vue-property-decorator";
import Login from "@/components/admin/Login.vue";
import AdminScreen from "@/components/admin/AdminScreen.vue";
import {AuthService} from "@/services/AuthService";
@Component({components: {Login, AdminScreen}})
export default class AdminContainer extends Vue {

  logined = false

  async created() {
    this.logined = await AuthService.isTokenValid()
  }
}
</script>

<style scoped>

</style>