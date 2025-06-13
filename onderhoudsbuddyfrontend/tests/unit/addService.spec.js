import { mount, flushPromises } from '@vue/test-utils'
import AddService from '@/views/AddService.vue'
import { vi } from 'vitest'

// Mocks
vi.mock('@/views/api.js', () => ({
    default: {
        getAllGarages: vi.fn(() =>
            Promise.resolve({
                data: [
                    { garageId: 1, name: 'Garage A' },
                    { garageId: 2, name: 'Garage B' }
                ]
            })
        ),
        createService: vi.fn(() => Promise.resolve())
    }
}))

vi.mock('vue-router', () => ({
    useRoute: () => ({
        params: { carId: '123' }
    }),
    useRouter: () => ({
        push: vi.fn()
    })
}))

describe('AddService.vue', () => {
    it('renders form and filters garages', async () => {
        const wrapper = mount(AddService)

        await flushPromises()

        const input = wrapper.find('#garage-search')
        await input.setValue('Garage A')

        await flushPromises()

        expect(wrapper.text()).toContain('Garage A')
        expect(wrapper.text()).not.toContain('Garage B')
    })

    it('shows error if trying to create service without garageId', async () => {
        const wrapper = mount(AddService)
        await flushPromises()

        await wrapper.vm.createService()
        expect(wrapper.text()).toContain('Selecteer een garage.')
    })
})
