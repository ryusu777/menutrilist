<template>
  <q-page class="q-mx-lg">
  
    <div class="row justify-center q-mt-xl">
      <h3 class="col-10 text-bold text-center q-my-sm">Search Food</h3>
      <form @submit.prevent="signIn" class="col-8 q-gutter-x-lg">
        <base-input
          class="login-input col-xs-11 col-sm-10"
          v-model="searchFood"
          debounce="500"
          placeholder="Search"
        >
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </base-input>
      </form>
    </div>
    <div class="column justify-center q-mt-lg">
      <q-table grid :rows="rows" row-key="id" :filter="filter" hide-header>
      <template v-slot:item="props">
        <div class="q-pa-xs col-12">
          <base-card
            :class="props.selected ? 'bg-grey-2' : ''"
          >
            <q-card-section horizontal class="row justify-start">
              <q-card-section class="col-9">
                <p class="text-bold text-h5 q-pa-none q-ma-none">
                  {{ props.row.name }}
                </p>
              </q-card-section>
              <q-card-action class="col-2 self-center align-right">
                <base-button label="Detail" color="primary" @click="showDetail" />
              </q-card-action>
            </q-card-section>
          </base-card>
        </div>
      </template>
    </q-table>
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import DetailDialog from 'src/components/search-food/DetailDialog.vue';
import { useQuasar } from 'quasar';
// import { EUserActions } from 'src/store/user/types/enumeration';

const columns = [
  {
    name: 'desc',
    required: true,
    label: 'Dessert (100g serving)',
    align: 'left',
    sortable: true
  },
  { name: 'calories', align: 'center', label: 'Calories', field: 'calories', sortable: true },
  { name: 'fat', label: 'Fat (g)', field: 'fat', sortable: true },
  { name: 'carbs', label: 'Carbs (g)', field: 'carbs' }
]

const rows = [
  {
    name: 'Frozen Yogurt',
    calories: 159,
    fat: 6.0,
    carbs: 24
  },
  {
    name: 'Ice cream sandwich',
    calories: 237,
    fat: 9.0,
    carbs: 37
  },
]
export default defineComponent({
  components: {
    BaseInput,
    BaseCard,
    BaseButton
  },
  // data() {
  //   return {
  //     userName: '' as string,
  //     email: '' as string,
  //     password: '' as string,
  //     confirmpassword: '' as string,
  //     searchFood: '' as string,
  //   };
  // },
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
setup () {
  const $q = useQuasar();

  function showDetail() {
      $q.dialog({
        component: DetailDialog,
        }
      );
    }
    return {
      filter: ref(''),
      columns,
      rows,
      showDetail,
    }
  }
});



</script>
