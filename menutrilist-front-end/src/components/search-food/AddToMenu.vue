<template>
  <q-page>
      <div class="row justify-center">
        <h3 class="col-12 text-bold text-center q-mb-lg">Add to Menu</h3>
        <q-date class="col-10" v-model="date" today-btn />
    </div>
  </q-page>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

export default defineComponent({
  components: {
  },
  setup() {
      

    return {
        date: ref('')
    };
  }
});
</script>
