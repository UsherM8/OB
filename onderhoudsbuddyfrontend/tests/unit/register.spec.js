import { mount, flushPromises } from '@vue/test-utils'
import Register from '@/views/Register.vue' // Adjust if needed
import { vi } from 'vitest'

// Mock API
vi.mock('@/views/api.js', () => ({
    default: {
        createUser: vi.fn(() => Promise.resolve())
    }
}))

// Mock vue-router
const push = vi.fn()

vi.mock('vue-router', () => ({
    useRouter: () => ({
        push
    })
}))

describe('Register.vue', () => {
    beforeEach(() => {
        vi.clearAllMocks()
    })

    it('submits valid form data', async () => {
        const api = (await import('@/views/api.js')).default
        const wrapper = mount(Register)

        await wrapper.find('input[name="firstName"]').setValue('John')
        await wrapper.find('input[name="lastName"]').setValue('Doe')
        await wrapper.find('input[name="birthDate"]').setValue('1990-01-01')
        await wrapper.find('input[name="email"]').setValue('john@example.com')
        await wrapper.find('input[name="password"]').setValue('Password123')

        await wrapper.find('form').trigger('submit.prevent')
        await flushPromises()

        expect(api.createUser).toHaveBeenCalledOnce()
        expect(api.createUser).toHaveBeenCalledWith({
            firstName: 'John',
            lastName: 'Doe',
            birthDate: '1990-01-01',
            email: 'john@example.com',
            password: 'Password123',
            type: 'USER'
        })

        expect(push).toHaveBeenCalledWith('/login')
    })

    it('shows error if fields are missing', async () => {
        const wrapper = mount(Register)
        await wrapper.find('form').trigger('submit.prevent')
        await flushPromises()

        expect(wrapper.text()).toContain('Vul alle velden in.')
    })
})
