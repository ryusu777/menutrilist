<template>
  <q-input outlined stack-label v-model="model" class="text-bold" type="date">
    <!-- <template v-slot:append>
        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy transition-show="scale" transition-hide="scale">
            <q-date v-model="date">
              <div class="row items-center justify-end">
                <q-btn v-close-popup label="Close" color="primary" flat />
              </div>
            </q-date>
          </q-popup-proxy>
        </q-icon>
      </template> -->
  </q-input>
</template>

<script lang="ts">
import { defineComponent, computed } from 'vue';

export default defineComponent({
  inheritAttrs: false,
  emits: ['modelValue:update'],
  props: {
    modelValue: {
      type: Date,
      required: false,
      default: new Date()
    }
  },
  setup(props, { emit }) {
    const model = computed({
      get(): Date {
        return props.modelValue;
      },
      set(val: Date) {
        emit('modelValue:update', val);
      }
    });
    return {
      model
    };
  }
});
</script>
