<template>
  <q-field v-model="content" dense>
    <slot></slot>
  </q-field>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  emits: ['input'],
  props: {
    value: {
      type: String,
      required: false,
      default: '',
    }
  },
  data() {
    return {
      content: this.value,
    };
  },
  watch: {
    content(value) {
      this.$emit('input', value);
    },
  },
});
</script>
