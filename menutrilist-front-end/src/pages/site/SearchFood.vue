<template>
  <q-page class="q-mx-lg">
    <base-card class="q-pa-md q-mt-xl" style="border-radius: 25px">
      <div class="row justify-center">
        <div class="col-12 q-gutter-x-lg">
          <base-input
            class="login-input text-body1"
            v-model="searchText"
            debounce="500"
            icon="search"
            placeholder="Search"
          >
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </base-input>
        </div>
      </div>
      <div
        class="column justify-center q-mt-lg q-gutter-y-sm"
        v-if="foodResponse"
      >
        <base-card
          style="min-height: 65px"
          v-for="food in foodResponse.foods.food"
          :key="food.food_id"
        >
          <q-card-section horizontal class="row justify-start">
            <q-card-section class="col-9">
              <p class="text-weight-bold text-h6 q-pa-none q-ma-none">
                {{ food.food_name }}
              </p>
            </q-card-section>
            <q-card-action class="col-2 self-center align-right">
              <base-button
                label="Detail"
                color="primary"
                @click="showDetail(food.food_id)"
              />
            </q-card-action>
          </q-card-section>
        </base-card>
      </div>
    </base-card>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue';
import BaseInput from 'components/ui/BaseInput.vue';
import BaseCard from 'src/components/ui/BaseCard.vue';
import BaseButton from 'src/components/ui/BaseButton.vue';
import DetailDialog from 'src/components/search-food/DetailDialog.vue';
import { useQuasar } from 'quasar';
import { IFoodSearch } from 'src/domain/interface';
import { api } from 'src/boot/axios';

export default defineComponent({
  components: {
    BaseInput,
    BaseCard,
    BaseButton,
  },
  setup() {
    const $q = useQuasar();

    const searchText = ref('');

    watch(
      () => searchText.value,
      async () => await sendSearchRequest()
    );

    const foodResponse = ref<IFoodSearch>();

    async function sendSearchRequest() {
      try {
        const response = await api.get<IFoodSearch>('fat-secret/search', {
          params: {
            searchWord: searchText.value,
            maxResults: 7,
          },
        });

        foodResponse.value = response.data;
      } catch (err) {
        $q.notify({
          message: 'Something went wrong',
        });
      }
    }

    function showDetail(foodId: number) {
      $q.dialog({
        component: DetailDialog,
      });
    }

    return {
      foodResponse,
      showDetail,
      searchText,
    };
  },
});
</script>
