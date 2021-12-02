<template>
  <q-checkbox v-model="checkBoxValue" id="checkbox" :label="label"/>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  emits: ['update:modelValue'],
  props: {
    modelValue: [Boolean, Array] as PropType<boolean | Array<string>>,
    label: String,
  },
  data() {
    return {
      checkBoxValue: this.modelValue,
    };
  },
  watch: {
    checkBoxValue(value: Array<string>): void {
      this.$emit('update:modelValue', value);
    },
  },
});
</script>

