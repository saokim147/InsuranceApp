class ToastManager {
    constructor() {
        this.notifications = [];
        this.displayDuration = 5000;

        this.container = document.createElement("div");
        this.container.className = "fixed top-5 right-5 z-50 flex flex-col space-y-3 max-w-xs";
        document.body.appendChild(this.container);

        window.addEventListener("notify", (event) => {
            this.addNotification(event.detail);
        });
    }

    addNotification({ variant = "info", title = "", message = "" }) {
        const id = Date.now();
        if (this.notifications.length >= 20) {
            this.dismissNotification(this.notifications[0]);
        }
        const notification = { id, variant, title, message };
        this.notifications.push(notification);
        notification.element = this.createNotificationElement(notification);
        this.container.appendChild(notification.element);
        notification.timeout = setTimeout(() => {
            this.dismissNotification(notification);
        }, this.displayDuration);
    }

    createNotificationElement(notification) {
        const color = this.getVariantStyles(notification.variant);
        const notificationHTML = `
        <div class="max-w-xs ${color} border border-gray-200 text-sm text-gray-800 rounded-lg" role="alert" tabindex="-1" aria-labelledby="hs-toast-soft-color-dark-label">
            <div id="hs-toast-soft-color-dark-label" class="flex p-4">
                ${notification.message}
                <div class="ms-auto">
                    <button type="button" class="inline-flex shrink-0 justify-center items-center size-5 rounded-lg text-gray-800 opacity-50 hover:opacity-100 focus:outline-hidden focus:opacity-100 dark:text-white" aria-label="Close">
                        <span class="sr-only">Close</span>
                        <svg class="shrink-0 size-4" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M18 6 6 18"></path>
                            <path d="m6 6 12 12"></path>
                        </svg>
                    </button>
                </div>
            </div>
        </div>`;

        const wrapper = document.createElement("div");
        wrapper.innerHTML = notificationHTML;
        const el = wrapper.firstElementChild;

        el.querySelector("button").addEventListener("click", (e) => {
            e.stopPropagation();
            this.dismissNotification(notification);
        });

        el.addEventListener("click", () => {
            this.dismissNotification(notification);
        });

        return el;
    }
    getVariantStyles(variant) {
        switch (variant) {
            case "success":
                return "bg-green-500 dark:bg-green-600 dark:text-white";
            case "danger":
                return "bg-red-500 dark:bg-red-600 dark:text-white";
            case "warning":
                return "bg-yellow-500 dark:bg-yellow-600 dark:text-black";
            case "info":
                return "bg-blue-500 dark:bg-blue-600 dark:text-white";
            case "message":
                return "bg-gray-600 dark:bg-gray-700 dark:text-white";
            default:
                return "bg-gray-800 dark:bg-gray-900 dark:text-white";
        }
    }
    dismissNotification(notification) {
        if (notification.element) {
            notification.element.classList.add("opacity-0", "transition-opacity", "duration-300");
            setTimeout(() => notification.element.remove(), 300);
        }
        clearTimeout(notification.timeout);
        this.notifications = this.notifications.filter((n) => n.id !== notification.id);
    }
    destroy(){
        this.notifications.forEach(notification => {
            clearTimeout(notification.timeout);
            if (notification.element) {
                notification.element.remove();
            }
        });
        this.notifications = [];

        if (this.container) {
            this.container.remove();
        }
        window.removeEventListener("notify", this.addNotification);
    }
}
const toast = new ToastManager();