<template>
  <div class="container">
    <div>
      <div class="block" style="text-align: center">
        <b-switch v-model="published">Опубликованные</b-switch>
        <b-switch v-model="hidden">Скрытые</b-switch>
      </div>

      <b-table
          :data="items"
          detailed
          show-detail-icon
          icon-pack="fas"
          style="text-align: left">
        <b-table-column label="Текст" width="30" v-slot="props">{{ props.row.text }}</b-table-column>
        <b-table-column label="Автор" width="10" v-slot="props">{{ props.row.author }}</b-table-column>
        <!--      <b-table-column label="Статус" width="40" v-slot="props">-->
        <!--        {{-->
        <!--          `${props.row.published ? "Опубликована" : "Неопубликована"} ${props.row.hidden ? "Скрыта" : "Отображается"}`-->
        <!--        }}-->
        <!--      </b-table-column>-->
        <template slot="detail" slot-scope="props">
          <PostEdit :post="props.row"></PostEdit>

        </template>
      </b-table>
    </div>
  </div>
</template>

<script>
import {Component, Vue} from "vue-property-decorator";
import {BSwitch} from "buefy/dist/components/switch"
import {BTable} from "buefy/dist/components/table"
import {BTableColumn} from "buefy/dist/components/table"
import {PostService} from "@/services/PostService";
import PostEdit from "@/components/admin/PostEdit";

@Component({
  components: {
    PostEdit,
    "b-switch": BSwitch,
    "b-table": BTable,
    "b-table-column": BTableColumn,
  }
})
export default class AdminScreen extends Vue {
  published = false
  hidden = false
  items_ = []


  async created() {
    this.items_ = await PostService.Instance.loadAdmin()
  }

  get items() {
    return this.items_.filter(x => x.published === this.published & x.hidden === this.hidden)
  }
}
</script>

<style scoped>

</style>