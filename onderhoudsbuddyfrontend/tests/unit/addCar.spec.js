import { mount, flushPromises } from '@vue/test-utils'
import AddCar from '@/views/CarRegister.vue' // adjust path if needed
import { vi } from 'vitest'

// Mock API
vi.mock('@/views/api.js', () => ({
    default: {
        addCar: vi.fn(() => Promise.resolve())
    }
}))

// Mock router
const push = vi.fn()
const back = vi.fn()

vi.mock('vue-router', () => ({
    useRouter: () => ({ push, back })
}))

describe('AddCar.vue', () => {
    beforeEach(() => {
        localStorage.clear()
        vi.resetAllMocks()
    })

    it('redirects to login if no token is found', async () => {
        mount(AddCar)
        await flushPromises()
        expect(push).toHaveBeenCalledWith('/login')
    })

    it('loads user ID from localStorage', async () => {
        localStorage.setItem('token', 'fake-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 42 }))

        const wrapper = mount(AddCar)
        await flushPromises()

        expect(wrapper.vm.userId).toBe(42)
    })

    it('shows validation error if form is incomplete', async () => {
        localStorage.setItem('token', 'fake-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 1 }))

        const wrapper = mount(AddCar)
        await flushPromises()

        await wrapper.find('form').trigger('submit.prevent')
        expect(wrapper.text()).toContain('Kenteken is verplicht')
    })

    it('submits form and calls API with correct data', async () => {
        localStorage.setItem('token', 'fake-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 123 }))

        const wrapper = mount(AddCar)
        await flushPromises()

        await wrapper.find('#licensePlate').setValue('AB-123-C')
        await wrapper.find('#mileage').setValue(15000)
        await wrapper.find('form').trigger('submit.prevent')

        const api = (await import('@/views/api.js')).default

        expect(api.addCar).toHaveBeenCalledWith({ licensePlate: 'AB-123-C', mileage: 15000 }, 123)
        expect(push).toHaveBeenCalledWith('/UserCar')
    })
})
