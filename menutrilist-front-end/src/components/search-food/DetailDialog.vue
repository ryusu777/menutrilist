<template>
  <q-page>
      <div class="justify-center q-mt-xl">
      <h3 class="text-bold text-center q-my-sm">Detail Food</h3>
      <h3 class="text-bold text-center text-primary q-mt-lg q-mb-none">Nutrition List</h3>
      <base-card class="q-ma-lg">
        <q-card-section>
            Protein
        </q-card-section>
      </base-card>
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent} from 'vue';
import BaseCard from 'components/ui/BaseCard.vue';

export default defineComponent({
  components: {
    BaseCard,
  },
  setup() {


    return {
    };
  }
});
</script>
