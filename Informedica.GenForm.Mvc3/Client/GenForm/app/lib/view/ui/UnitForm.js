Ext.define('GenForm.lib.view.ui.UnitForm', {
    extend: 'GenForm.lib.view.form.FormBase',

    height: 326,
    width: 700,
    bodyPadding: 10,

    initComponent: function() {
        var me = this;

        me.items = [
            {
                xtype: 'fieldset',
                itemId: 'flsUnitDetails',
                title: 'Eenheid Details',
                items: [
                    {
                        xtype: 'hiddenfield',
                        itemId: 'fldId',
                        name: 'Id',
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        itemId: 'fldName',
                        name: 'Name',
                        fieldLabel: 'Naam',
                        anchor: '100%'
                    },
                    {
                        xtype: 'comboboxcontainer',
                        editAction: 'editUnitGroup',
                        addAction: 'addUnitGroup',
                        comboBox: me.createComboBox({
                            idName: true,
                            itemId: 'cboUnitGroup',
                            fieldLabel: 'Eenheid groep',
                            name: 'UnitGroup',
                            hideEmptyLabel: false,
                            flex: 1,
                            directFn: GenForm.server.UnitTest.GetUnitGroupNames
                        })
                    }
                ]
            },
            {
                xtype: 'tabpanel',
                activeTab: 0,
                layout: 'anchor',
                anchor: '100%',
                maintainFlex: true,
                listeners: {
                    tabChange: {
                        fn: me.onTabChangeLoadStore,
                        scope: me
                    }
                },
                items: [
                    me.createGrid()
                ]
            }
        ];
        me.callParent(arguments);
    }
});