namespace InsuranceWebApp.Helper
{
    public class Css
    {
        public const string Input = "w-full items-center rounded-md border border-gray-200 py-2.5 sm:py-3 px-4 focus:outline-none focus:ring-2 focus:ring-neutral-300 focus:border-transparent dark:bg-neutral-800 dark:border-neutral-700 dark:text-neutral-400 dark:placeholder-neutral-500 dark:focus:ring-neutral-600";
        public const string Btn = "shrink-0 inline-flex h-12 min-h-[3rem] cursor-pointer select-none flex-wrap items-center justify-center gap-2 rounded-lg border border-transparent px-4 text-center text-sm focus:ring-1 focus:outline-none disabled:opacity-50 disabled:pointer-events-none text-sm";
        public const string Primary = "bg-primary-500 text-white hover:bg-primary-600 focus:ring-primary-500 focus:ring-2";
        public const string Secondary = "bg-secondary-500 text-white hover:bg-secondary-600 focus:ring-secondary-500 focus:ring-2";
        public const string BtnPrimary = $"{Btn} {Primary}";
        public const string BtnSecondary = $"{Btn} {Secondary}";
        public const string GotoBox = " block min-h-[38px] w-12 rounded-lg border border-gray-200 px-2.5 py-2 text-center text-sm disabled:opacity-50 disabled:pointer-events-none dark:bg-neutral-800 dark:border-neutral-700 dark:text-neutral-400 dark:placeholder-neutral-500 dark:focus:ring-neutral-600 [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none";
        public const string TableHeader = "px-5 py-3 text-start text-sm font-bold uppercase dark:text-neutral-500";
        public const string TableRow = "whitespace-nowrap px-5 py-4 text-sm font-medium text-gray-800 dark:text-neutral-200";

        public const string TableRowFixed = "whitespace-nowrap break-words px-5 py-4 text-sm font-medium text-gray-800 dark:text-neutral-200";
    }
}
