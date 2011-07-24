Ext.define('GenForm.controller.mixin.SubstanceHandler', {

    editOrAddSubstance: function () {
        var me = this;
        me.getSubstanceWindow().show();
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
        return Ext.ModelManager.create({}, 'GenForm.model.product.SubstanceName');
    },

    onSubstanceSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('substancewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Substance saved: ', result.data.SubstanceName);
            me.addUnitToStore(result.data.SubstanceName);
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

        Product.AddNewSubstance(substance.data, {scope: me, callback:me.onSubstanceSaved});
    }

});