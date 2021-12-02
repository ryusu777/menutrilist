<template>
    <q-select 
        borderless
        style="background-color: white; border-radius: 25px;"
        class="q-px-md"
        v-model="model" 
        :options="options" />
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

export default defineComponent({
  setup () {
    return {
      model: ref(null),
      options: [
        'Option 1', 'Option 2', 'Option 3'
      ]
    }
  }
});
</script>
