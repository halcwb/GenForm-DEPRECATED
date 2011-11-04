Ext.define('GenForm.controller.mixin.SubstanceHandler', {

    addSubstanceToStore: function (substance) {
        var me = this,
            store = me.getSubstanceStore();
        store.add({Name: substance});
    },

    getSubstanceStore: function () {
        var window = Ext.ComponentQuery.query('productsubstancewindow')[0];

        return window.forms.ProductSubstanceForm.fields.Substance.store;
    },

    createSubstanceWindow: function () {
        return Ext.create(this.getProductSubstanceWindowView());
    },

    onAddSubstance: function () {
        var me = this,
            window = me.getSubstanceWindow(me.createEmptySubstance()).show();

        window.setTitle('Nieuwe Stof');
        window.show();
    },

    onEditSubstance: function (button) {
        var me = this,
            form = button.findParentByType('productsubstanceform'),
            Substance = form.fields.Substance.findRecord('Name', form.fields.Substance.getValue()),
            window = me.getSubstanceWindow(Substance);

        window.show();
    },

    getSubstanceWindow: function () {
        var me = this, form;

        form = me.createSubstanceWindow();
        me.loadEmptySubstance(form);
        return form;
    },

    loadEmptySubstance: function (window) {
        var me = this;
        window.loadWithSubstance(me.createEmptySubstance());
    },

    createEmptySubstance: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Substance');
    },

    onSubstanceSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('substancewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Substance saved: ', result.data.Name);
            me.addSubstanceToStore(result.data.Name);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Substance could not be saved: ', result.message);
        }
    },

    getSubstance: function (button) {
        return button.up('panel').down('form').getSubstance();
    },

    saveSubstance: function (button) {
        var me = this,
            substance = me.getSubstance(button);

        GenForm.server.Product.AddNewSubstance(substance.data, {scope: me, callback:me.onSubstanceSaved});
    }

});