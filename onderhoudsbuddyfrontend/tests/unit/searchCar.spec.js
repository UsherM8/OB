import { mount, flushPromises } from '@vue/test-utils'
import SearchCar from '@/views/SearchCar.vue'
import { vi } from 'vitest'

vi.mock('@/views/api.js', () => ({
    default: {
        getCarByLicense: vi.fn(() => Promise.resolve({ data: { licensePlate: 'AB-123-C' } }))
    }
}))

describe('SearchCar.vue', () => {
    it('forces license plate to uppercase and calls API with correct value', async () => {
        const api = (await import('@/views/api.js')).default
        const wrapper = mount(SearchCar)
        const input = wrapper.find('input[type="text"]')
        await input.setValue('ab-123-c')
        await wrapper.find('button').trigger('click')
        await flushPromises()
        expect(wrapper.vm.licensePlate).toBe('AB-123-C')
        expect(api.getCarByLicense).toHaveBeenCalledWith('AB-123-C')
        expect(wrapper.text()).toContain('AB-123-C')
    })

    it('shows error when license plate is empty', async () => {
        const wrapper = mount(SearchCar)
        await wrapper.find('button').trigger('click')
        await flushPromises()
        expect(wrapper.text()).toContain('Voer een kenteken in')
    })
})

