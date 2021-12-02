<template>
  <div>
    <slot></slot>
    <q-input 
      :type="inputVisible ? 'text' : 'password'" 
      v-model="model" 
      outlined
      dense
      class="input"
      v-bind="$attrs"
    >
      <template #append v-if="password">
        <q-btn
          :icon="inputVisible ? 'visibility' : 'visibility_off'"
          flat
          class="q-px-xs q-py-none"
          @click="inputVisible = !inputVisible"
          tabindex="-1"
        ></q-btn>
      </template>
      <template #append v-else>
        <slot name="append"></slot>
      </template>
    </q-input>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  emits: ['modelValue:update'],
  inheritAttrs: false,
  props: {
    modelValue: {
      type: String,
      required: false,
      default: '',
    },
    password: Boolean,
  },
  data() {
    return {
      content: this.value,
      inputVisible: !this.password,
    };
  },
  computed: {
    model: {
      get() {
        return this.modelValue;
      },
      set(value: string) {
        this.$emit('modelValue:update', value);
      }
    }
  },
});
</script>
