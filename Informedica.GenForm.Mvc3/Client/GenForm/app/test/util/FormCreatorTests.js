Ext.define('GenForm.test.util.FormCreatorTests', {
    describe: 'FormCreatorShould',

    tests: function () {
        var me = this, creator,
            namespace = 'GenForm.test.util.formcreatortests.',
            testWindow = namespace + 'TestWindow',
            formConfig = {name: 'TestForm', title: 'TestForm'};

        Ext.define(testWindow, {
            extend: 'Ext.window.Window',
            mixins: ['GenForm.lib.util.mixin.FormCreator'],
            initComponent: function () {
                var me = this;
                me.forms = {};
                me.items = me.createItems();
                me.callParent(arguments);
            },

            createItems: function () {
                var me = this;
                return me.createForm(formConfig);
            }
        });

        me.getTestWindow = function () {
            return Ext.create(testWindow, {title: 'TestWindow'});
        };

        beforeEach(function () {
            if (!creator) creator = Ext.create('GenForm.lib.util.mixin.FormCreator');
            return creator;
        });

        it('be defined', function () {
            expect(creator).toBeDefined();
        });

        it('can create a form', function () {
            expect(creator.createForm(formConfig)).toBeDefined();
        });

        it('ads the newly created form to the container', function () {
            expect(me.getTestWindow().forms[formConfig.name]).toBeDefined();
        });

        it('can create a form from xtype', function () {
            expect(creator.getConstructor({xtype: 'test'})).toBe('widget.test');
        });

        it('does not prepend widget when xtype is widget', function () {
            expect(creator.getConstructor({xtype: 'widget.test'})).toBe('widget.test');
        });

    }
});