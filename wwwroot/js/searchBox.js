class AutoCompleteHandler {
    constructor(inputId, listId,hiddenName='') {
        this.input = document.getElementById(inputId);
        this.list = document.getElementById(listId);
        this.hiddenInput = document.querySelector(`input[type="hidden"][name="${hiddenName}"]`) || null;
        this.currentIndex = -1;
        this.bindEvents();
    }
    destroy() {
        this.input.removeEventListener("keydown", this.handleKeyDown);
        this.input.removeEventListener("input", this.handleInput);
        this.list.removeEventListener("click", this.handleClick);
        this.list.removeEventListener("htmx:afterSwap", this.handleAfterSwap);
        document.removeEventListener("click", this.handleDocumentClick);
        this.input = null;
        this.list = null;
        this.hiddenInput = null;
        
    }
    get value() {
        return this.input ? this.input.value : '';
    }
    get hiddenValue() {
        return this.hiddenInput ? this.hiddenInput.value : '';
    }

    closeList() {
        this.list.classList.add("hidden");
        this.currentIndex = -1;
    }
    showList() {
        if (this.list.innerHTML.trim() !== "") {
            this.list.classList.remove("hidden");
        }
    }
    highlight(options) {
        options[this.currentIndex].classList.add("bg-gray-200");
    }
    scrollIntoViewIfNeeded(element) {
        const container = this.list;
        const containerHeight = container.clientHeight;
        const scrollTop = container.scrollTop;
        const elementOffset = element.offsetTop;
        const elementHeight = element.offsetHeight;

        if (elementOffset < scrollTop) {
            container.scrollTop = elementOffset;
        } else if (elementOffset + elementHeight > scrollTop + containerHeight) {
            container.scrollTop = elementOffset - containerHeight + elementHeight;
        }
    }
    moveSelection(direction) {
        const options = this.list.querySelectorAll(".search-option");
        if (options.length === 0) return;
        if (this.currentIndex !== -1 && options[this.currentIndex]) {
            options[this.currentIndex].classList.remove("bg-gray-200");
        }
        if (direction === "down") {
            this.currentIndex = (this.currentIndex + 1) % options.length;
        } else {
            this.currentIndex = (this.currentIndex - 1 + options.length) % options.length;
        }
        this.highlight(options);
        this.scrollIntoViewIfNeeded(options[this.currentIndex]);
    }

    bindEvents() {
        // Handle keyboard navigation and Escape key
        this.input.addEventListener("keydown", (e) => {
            switch (e.key) {
                case "ArrowDown":
                    e.preventDefault();
                    this.moveSelection("down");
                    break;
                case "ArrowUp":
                    e.preventDefault();
                    this.moveSelection("up");
                    break;
                case "Escape":
                    this.closeList();
                    break;
            }
        });

        // Show dropdown on input
        this.input.addEventListener("input", () => {
            //console.log(this.input.value.length);
            if (this.input.value.trim().length > 0) {
                this.showList();
            } else {
                this.closeList();
            }
        });

        // Select individual item on click
        this.list.addEventListener("click", (e) => {
            const option = e.target.closest(".search-option");
            if (option) {
                this.input.value = option.textContent.trim();
                if (this.hiddenInput && option.dataset.value) {
                    this.hiddenInput.value = option.dataset.value;
                }
                this.closeList();
            }
        });

        this.list.addEventListener("htmx:afterSwap", () => {
            const hasOptions =
                this.list.querySelectorAll(".search-option").length > 0;
            if (hasOptions) {
                this.showList();
            } else {
                this.closeList();
            }
        });

        document.addEventListener("click", (e) => {
            if (!this.list.contains(e.target) && e.target !== this.input) {
                this.closeList();
            }
        });
    }
}
