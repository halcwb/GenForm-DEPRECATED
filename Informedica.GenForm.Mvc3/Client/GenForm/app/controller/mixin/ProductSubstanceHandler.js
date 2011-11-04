//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.controller.mixin.ProductSubstanceHandler', {

    mixins: {
        substanceHandler: 'GenForm.controller.mixin.SubstanceHandler'
    },

    onAddProductSubstance: function () {
        var me = this;
        me.showProductSubstanceWindow();
    },

    showProductSubstanceWindow: function () {
        var me = this;
        me.getProductSubstanceWindow().show();
    },

    loadEmptyProductSubstance: function (window) {
        window.loadWithSubstance(this.createEmptyProductSubstance());
    },

    getProductSubstanceStore: function () {
        var me = this;
        return me.getProductProductSubstanceStore();
    },

    getProductSubstance: function (button) {
        return button.up('panel').down('form').getProductSubstance()
    },

    getProductSubstanceWindow: function () {
        var me = this, window;

        window = me.createProductSubstanceWindow();
        me.loadEmptyProductSubstance(window);
        return window;
    },

    createProductSubstanceWindow: function () {
        var me = this;
        return Ext.create(me.getProductProductSubstanceWindowView());
    },

    addProductSubstanceToStore: function (substance) {
        var me = this,
            store = me.getProductSubstanceStore();

        store.add({UnitName: substance});
    },

    createEmptyProductSubstance: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.ProductSubstance');
    }
    
});