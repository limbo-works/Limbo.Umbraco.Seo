import { defineConfig } from 'vite';

// The bundle is emitted straight into "../wwwroot", which the Razor SDK serves as static web
// assets under "/App_Plugins/Limbo.Umbraco.Seo". Everything under "@umbraco-cms/backoffice" is
// left external: the backoffice provides those modules at runtime through its import map.
export default defineConfig({
    base: '/App_Plugins/Limbo.Umbraco.Seo/',
    build: {
        lib: {
            entry: 'src/manifests.ts',
            formats: ['es']
        },
        outDir: '../wwwroot',
        emptyOutDir: true,
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco/],
            // The build output is committed and shipped inside the NuGet package, so file names are
            // kept free of content hashes to avoid a churn of new files on every rebuild.
            output: {
                entryFileNames: 'limbo-seo.js',
                chunkFileNames: '[name].js',
                assetFileNames: '[name].[ext]'
            }
        }
    }
});
