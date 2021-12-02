<template>
  <q-page class="row items-center justify-evenly">
      <div>
          <div class="col-8 items-start">
            <base-button color="primary">
                <p class="q-ma-none test-bold" color="white">Breakfast</p>
                <q-icon class="col-2 q-ml-md" name="add" color="white" />
            </base-button>
          </div>
          <div class="col-8 q-mt-md items-start">
            <base-button color="primary">
                <p class="q-ma-none test-bold" color="white">Lunch</p>
                <q-icon class="q-ml-lg" name="add" color="white" />
            </base-button>
          </div>
          <div class="col-8 q-mt-md items-start">
            <base-button color="primary">
                <p class="q-ma-none test-bold" color="white">Dinner</p>
                <q-icon class="q-ml-lg" name="add" color="white" />
            </base-button>
          </div>
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseButton from 'src/components/ui/BaseButton.vue';

export default defineComponent({
  components: {
      BaseButton,
  },
});
</script>
