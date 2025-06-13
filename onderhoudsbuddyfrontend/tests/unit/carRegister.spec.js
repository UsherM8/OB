import { mount, flushPromises } from '@vue/test-utils'
import AddCar from '@/views/CarRegister.vue' // Adjust path if needed
import { vi } from 'vitest'

const push = vi.fn()
const back = vi.fn()

vi.mock('vue-router', () => ({
    useRouter: () => ({ push, back })
}))

vi.mock('@/views/api.js', () => ({
    default: {
        addCar: vi.fn(() => Promise.resolve())
    }
}))

describe('AddCar.vue', () => {
    beforeEach(() => {
        vi.clearAllMocks()
        localStorage.clear()
    })

    it('adds a car successfully when form is valid', async () => {
        localStorage.setItem('token', 'mock-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 42 }))
        const api = (await import('@/views/api.js')).default

        const wrapper = mount(AddCar)
        await flushPromises()

        await wrapper.find('input[name="licensePlate"]').setValue('AB-123-C')
        await wrapper.find('input[name="mileage"]').setValue(12345)
        await wrapper.find('form').trigger('submit.prevent')
        await flushPromises()

        expect(api.addCar).toHaveBeenCalledOnce()
        expect(api.addCar).toHaveBeenCalledWith(
            { licensePlate: 'AB-123-C', mileage: 12345 },
            42
        )

        expect(push).toHaveBeenCalledWith('/UserCar')
    })

    it('shows error if license plate is empty', async () => {
        localStorage.setItem('token', 'mock-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 42 }))

        const wrapper = mount(AddCar)
        await flushPromises()

        await wrapper.find('input[name="mileage"]').setValue(10000)
        await wrapper.find('form').trigger('submit.prevent')
        await flushPromises()

        expect(wrapper.text()).toContain('Kenteken is verplicht')
    })

    it('shows error if mileage is 0 or negative', async () => {
        localStorage.setItem('token', 'mock-token')
        localStorage.setItem('userInfo', JSON.stringify({ id: 42 }))

        const wrapper = mount(AddCar)
        await flushPromises()

        await wrapper.find('input[name="licensePlate"]').setValue('XX-999-X')
        await wrapper.find('input[name="mileage"]').setValue(0)
        await wrapper.find('form').trigger('submit.prevent')
        await flushPromises()

        expect(wrapper.text()).toContain('Kilometerstand moet groter zijn dan 0')
    })

    it('redirects to login if no token is found', async () => {
        const wrapper = mount(AddCar)
        await flushPromises()

        expect(push).toHaveBeenCalledWith('/login')
    })
})
